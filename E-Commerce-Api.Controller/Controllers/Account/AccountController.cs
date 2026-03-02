using E_Commerce.APIs.Controllers.Base;
using E_Commerce.App.Application.Abstruction.Models.Auth;
using E_Commerce.App.Application.Abstruction.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Api.Controller.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : BaseApiController
    {

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var result = await serviceManager.AuthService.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var result = await serviceManager.AuthService.RegisterAsunc(model);
            return Ok(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgatPasswordDto model)
        {
            var url = Url.Action("ResetPassword", "Account", new { email = model.Email }, Request.Scheme);
            await serviceManager.AuthService.ForgotPasswordAsync(model ,url);
            return Ok();

        }
        [HttpPost("ResetPassword/{token}")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto model)
        {
            var token = Request.Query["token"].ToString();
            await serviceManager.AuthService.ResetPasswordAsync(model ,token);
            return Ok();
        }
    }
}
