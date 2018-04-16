using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Repositories;
using HomeSweetHomeServer.Services;
using HomeSweetHomeServer.Exceptions;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HomeSweetHomeServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public IInformationRepository _informationRepository;
        public IUserRepository _userRepository;
        public IUserInformationRepository _userInformationRepository;
        public IConfiguration _config;

        public AuthenticationService(IInformationRepository informationRepository,
                                     IUserRepository userRepository,
                                     IUserInformationRepository userInformationRepository,
                                     IConfiguration config)
        {
            _informationRepository = informationRepository;
            _userRepository = userRepository;
            _userInformationRepository = userInformationRepository;
            _config = config;
        }
                      
        public async Task ControlRegisterFormAsync(RegistrationModel registrationForm)
        {
            CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
            AuthenticationModel authentication = new AuthenticationModel();
            authentication.Username = registrationForm.Username;

            var UsernameExist = await _userRepository.GetByUsernameAsync(authentication.Username);
            if (UsernameExist != null)
                errors.AddError("Username", "Username already exist");
            
            var EmailExist = await _userInformationRepository.GetUserInformationByValueAsync(registrationForm.Email);
            if (EmailExist != null)
                errors.AddError("Email", "Email already exist");

            var PhoneNumberExist = await _userInformationRepository.GetUserInformationByValueAsync(registrationForm.PhoneNumber);
            if (PhoneNumberExist != null)
                errors.AddError("PhoneNumber", "PhoneNumber already exist");

            if (errors.Errors.Count != 0)
                errors.Throw();
        }

        public async Task RegisterNewUser(RegistrationModel registrationForm)
        {
            AuthenticationModel authentication = new AuthenticationModel();
            InformationModel info = new InformationModel();
            UserInformationModel userInformation = new UserInformationModel();

            authentication.Username = registrationForm.Username;
            authentication.Password = registrationForm.Password;
            authentication.IsVerifiedByEmail = false;

            await _userRepository.InsertAsync(authentication);

            info = await _informationRepository.GetInformationByInformationNameAsync("FirstName");
            userInformation.Information = info;
            userInformation.User = authentication;
            userInformation.Value = registrationForm.FirstName;
            await _userInformationRepository.InsertAsync(userInformation);
            userInformation = null;

            userInformation = new UserInformationModel();
            info = await _informationRepository.GetInformationByInformationNameAsync("LastName");
            userInformation.Information = info;
            userInformation.User = authentication;
            userInformation.Value = registrationForm.LastName;
            await _userInformationRepository.InsertAsync(userInformation);
            userInformation = null;
            
            userInformation = new UserInformationModel();
            info = await _informationRepository.GetInformationByInformationNameAsync("Email");
            userInformation.Information = info;
            userInformation.User = authentication;
            userInformation.Value = registrationForm.Email;
            await _userInformationRepository.InsertAsync(userInformation);
            userInformation = null;

            userInformation = new UserInformationModel();
            info = await _informationRepository.GetInformationByInformationNameAsync("PhoneNumber");
            userInformation.Information = info;
            userInformation.User = authentication;
            userInformation.Value = registrationForm.PhoneNumber;
            await _userInformationRepository.InsertAsync(userInformation);
            userInformation = null;

            userInformation = new UserInformationModel();
            info = await _informationRepository.GetInformationByInformationNameAsync("RegistrationDate");
            userInformation.Information = info;
            userInformation.User = authentication;
            userInformation.Value = String.Format("{0:u}", registrationForm.RegistrationDate);
            await _userInformationRepository.InsertAsync(userInformation);

            userInformation = new UserInformationModel();
            info = await _informationRepository.GetInformationByInformationNameAsync("IsEmailVerified");
            userInformation.Information = info;
            userInformation.User = authentication;
            userInformation.Value = "false";
            await _userInformationRepository.InsertAsync(userInformation);
        }
        
        public async Task<AuthenticationModel> GetUser(LoginModel login)
        {
            AuthenticationModel user = await _userRepository.GetByUsernameAsync(login.Username);
            CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);

            if (user == null)
            {
                errors.AddError("Username", "Username is not registered");
                errors.Throw();
            }

            if(user.Password != login.Password)
            {
                errors.AddError("Password", "Username and password is not matched");
                errors.Throw();
            }
            return user;
        }

        //public async Task<>
    }
}
