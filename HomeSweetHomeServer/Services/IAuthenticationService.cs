using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Services
{
    //Interface about authentication operations, register, login, verify email, forgot password
    public interface IAuthenticationService
    {
        Task ControlRegisterFormAsync(RegistrationModel registrationForm);
        Task RegisterNewUserAsync(RegistrationModel registrationForm);
        Task<UserModel> Login(LoginModel login);
        Task<UserModel> GetUserFromId(int id, bool include = false);
        Task SendEmailVerificationCodeToUserAsync(UserModel user);
        Task SendForgotPasswordVerificationCodeToUserAsync(UserModel user);
        Task VerifyEmailAsync(UserModel user, VerificationCodeModel verificationCode);
        Task ForgotPasswordAsync(UserModel user, ForgotPasswordModel forgotPassword);
        Task<UserModel> GetUserFromMail(string email);
        Task<UserFullInformationModel> GetUserFullInformationAsync(int id);
    }
}
