using E_Commerce.App.Application.Abstruction.Models.Auth;
using E_Commerce.App.Application.Abstruction.Services.Auth;
using E_Commerce.App.Application.Exception;
using E_Commerce.App.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Service.Auth
{
    internal class AuthService(UserManager<ApplicationsUser> userManager, SignInManager<ApplicationsUser> signinManger) : IAuthService
    {
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
                Token = "will be token"
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
                Token = "will be token"
            };

        }
    }
}
