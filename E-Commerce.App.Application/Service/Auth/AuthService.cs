using E_Commerce.App.Application.Abstruction.Models.Auth;
using E_Commerce.App.Application.Abstruction.Services.Auth;
using E_Commerce.App.Application.Exception;
using E_Commerce.App.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc.Core;

namespace E_Commerce.App.Application.Service.Auth
{
    public class AuthService(
        IOptions<JWTSettings> JwtSetting,
        UserManager<ApplicationsUser> userManager,
        SignInManager<ApplicationsUser> signinManger,
        IEmailService _emailService) : IAuthService
    {

        private readonly JWTSettings _jwtSettings = JwtSetting.Value;
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedExeption("invalid login");

            var result = await signinManger.CheckPasswordSignInAsync(user, loginDto.Password, true);

            if (result.IsLockedOut) throw new UnAuthorizedExeption("Account is locked out");
            if (result.IsNotAllowed) throw new UnAuthorizedExeption("Account is not allowed to login");


            if (!result.Succeeded) throw new UnAuthorizedExeption("invalid login");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                DisablayName = user.DisableName,
                Token = await GeneratTokenAsync(user)
            };
        }
        public async Task<UserDto> RegisterAsunc(RegisterDto registerDto)
        {
            var user = new ApplicationsUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisableName = registerDto.DisplayName,
                PhoneNumber = registerDto.Phone
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) throw new ValidationExeption() { Errors = result.Errors.Select(E=>E.Description)};

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                DisablayName = user.DisableName,
                Token = await GeneratTokenAsync(user)
            };

        }


        public async Task ForgotPasswordAsync(ForgatPasswordDto dto ,string url)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null) throw new NotFoundException("user not found",dto.Email);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var email = new 
            {
                To = user.Email!,
                Subject = "Reset Password",
                Body = $"Please reset your password by clicking <a href='{url}?email={user.Email}&token={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))}'>here</a>"
            };
            
             _emailService.SendEmail(email.To, email.Subject, email.Body);

        }
        public async Task ResetPasswordAsync(ResetPasswordDto dto,string token)
        {
            if(dto.NewPassword != dto.ConfirmPassword)
                throw new ValidationExeption() { Errors = new List<string> { "New password and confirm password do not match." } };

            var user =await userManager.FindByEmailAsync(dto.Email);
            
            if (user is null) throw new NotFoundException("user not found", dto.Email);
            

            var result = await userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (!result.Succeeded)
            
                throw new ValidationExeption() { Errors =  result.Errors.Select(e => e.Description) };
        }
       

        private async Task<string> GeneratTokenAsync(ApplicationsUser user)
        {
            var UserClaims = await userManager.GetClaimsAsync(user);
            var userRoles = new List<Claim>();

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                userRoles.Add(new Claim(ClaimTypes.Role , role.ToString()));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid , user.Id),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.GivenName , user.DisableName),
            }.Union(userRoles)
            .Union(UserClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var TokenObj = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DuerationInMinutes),
                claims: claims,
                signingCredentials: signinCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(TokenObj);

        }

        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto dto)
        {
            //var settings = new GoogleJsonWebSignature.ValidationSettings
            //{
            //    Audience = new[] { _configuration["Authentication:Google:ClientId"] }
            //};                  

            var payLoad =await GoogleJsonWebSignature.ValidateAsync(dto.IdToken);

            var email = payLoad.Email;
            var name = payLoad.Name;

            if (email is null) throw new UnAuthorizedExeption("invalid google token");
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new ApplicationsUser
                {
                    DisableName = name ?? email.Split('@')[0],
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded) throw new ValidationExeption() { Errors = result.Errors.Select(e => e.Description) };
      
            }
      
            var token =await GeneratTokenAsync(user);

            return new AuthResponseDto
            {
                Email = user.Email!,
                Token = token
            };
        }
    }
}
