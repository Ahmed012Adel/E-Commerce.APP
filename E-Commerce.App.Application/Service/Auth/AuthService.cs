using E_Commerce.App.Application.Abstruction.Common;
using E_Commerce.App.Application.Abstruction.Models.Auth;
using E_Commerce.App.Application.Abstruction.Services.Auth;
using E_Commerce.App.Application.Exception;
using E_Commerce.App.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Service.Auth
{
    public class AuthService(
        IOptions<JWTSettings> JwtSetting,
        UserManager<ApplicationsUser> userManager,
        SignInManager<ApplicationsUser> signinManger) : IAuthService
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
    }
}
