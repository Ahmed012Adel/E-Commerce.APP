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
            //var url = Url.Action(
            //    "ResetPassword",       // Action
            //    "Account",             // Controller
            //    values: new {Email = model.Email},          // Query params مش هنا
            //    protocol: Request.Scheme, // http أو https
            //    host: Request.Host.Value // hostname + port
            
            //    );
            await serviceManager.AuthService.ForgotPasswordAsync(model);
            return Ok("Check Your Mail");

        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto model ,[FromQuery]string otp)
        {
            await serviceManager.AuthService.ResetPasswordAsync(model,otp );
            return Ok("Password Saved");
        }

        [HttpPost("VerifyEmail")]
        public async Task<ActionResult> VerifyEmail(VerifyOtpDto model)
        {
            await serviceManager.AuthService.VerifyEmail(model);
            return Ok("Email verified successfully");

        }

        [HttpPost("ResendOtp")]
        public async Task<ActionResult> ResendOTP(ForgatPasswordDto model)
        {
            await serviceManager.AuthService.ResendOTP(model);
            return Ok("OTP resent successfully");
        }
    }
}
