using E_Commerce.App.Application.Abstruction.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Abstruction.Services.Auth
{
    public interface IAuthService
    {

        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsunc(RegisterDto registerDto);


        Task ForgotPasswordAsync(ForgatPasswordDto dto ,string url);
        Task ResetPasswordAsync(ResetPasswordDto dto ,string Token);
        Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginDto dto);

    }
}
