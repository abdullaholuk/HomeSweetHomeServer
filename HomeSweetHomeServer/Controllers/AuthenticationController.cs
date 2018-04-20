using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Services;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using HomeSweetHomeServer.Exceptions;
using Newtonsoft.Json;
using System.Web;
using System.Net;

namespace HomeSweetHomeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    
    public class AuthenticationController : Controller
    {
        IAuthenticationService _authenticationService;
        IJwtTokenService _jwtTokenService;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenService jwtTokenService)
        {
            _authenticationService = authenticationService;
            _jwtTokenService = jwtTokenService;
        }

        //Registers sended user
        [HttpPost("Register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel registrationForm)
        {
            registrationForm.RegistrationDate = DateTime.UtcNow;

            await _authenticationService.ControlRegisterFormAsync(registrationForm);
            await _authenticationService.RegisterNewUserAsync(registrationForm);

            return Ok();
        }

        //Logins to server and accepts JWT token for requests
        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            UserModel user = await _authenticationService.GetUserAfterLoginAsync(login);
            string token = _jwtTokenService.CreateToken(user);

            if (user.IsVerifiedByEmail == false)
                return StatusCode((int)HttpStatusCode.Accepted, token);
            else
                return Ok(token);
        }

        //Requests for send email verification code to email
        [Authorize]
        [HttpGet("EMailVerification", Name = "EMailVerification")]
        public async Task<IActionResult> EMailVerification()
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            var user = await _jwtTokenService.GetUserFromTokenStr(token);
            if(user.IsVerifiedByEmail == true)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Already Verified", "User already verified");
                errors.Throw();
            }
            await _authenticationService.SendEmailVerificationCodeToUserAsync(user);
            return Ok();
        }

        //Verifies email
        [Authorize]
        [HttpPost("VerifyEmail", Name = "VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerificationCodeModel verificationCode)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            var user = await _jwtTokenService.GetUserFromTokenStr(token);

            if (user.IsVerifiedByEmail == true)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Already Verified", "User already verified");
                errors.Throw();
            }

            await _authenticationService.VerifyEmailAsync(user, verificationCode);

            return Ok();
        }

        //Requests for forgot password verification code
        [HttpGet("ForgotPassword", Name = "ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromHeader] string email)
        {
            UserModel user = await _authenticationService.GetUserFromMail(email);

            if(user.IsVerifiedByEmail == false)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("None Verified Email", "Your email is not verified");
                errors.Throw();
            }

            await _authenticationService.SendForgotPasswordVerificationCodeToUserAsync(user);

            return Ok();
        }

        //Changes password
        [HttpPost("ChangePassword", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ForgotPasswordModel forgotPassword)
        {
            UserModel user = await _authenticationService.GetUserFromMail(forgotPassword.Email);

            if (user.IsVerifiedByEmail == false)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("None Verified Email", "Your email is not verified");
                errors.Throw();
            }

            await _authenticationService.ForgotPasswordAsync(user, forgotPassword);

            return Ok();
        }
    }
}
