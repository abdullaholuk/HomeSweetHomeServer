using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Services
{
    public interface IAuthenticationService
    {
        Task ControlRegisterFormAsync(RegistrationModel registrationForm);
        Task RegisterNewUser(RegistrationModel registrationForm);
        Task<AuthenticationModel> GetUser(LoginModel login);
    }
}
