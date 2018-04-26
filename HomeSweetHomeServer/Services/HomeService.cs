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
    public class HomeService : IHomeService
    {
        public IInformationRepository _informationRepository;
        public IUserRepository _userRepository;
        public IUserInformationRepository _userInformationRepository;
        public IConfiguration _config;
        public IMailService _mailService;
        public IHomeRepository _homeRepository;

        public HomeService(IInformationRepository informationRepository,
                           IUserRepository userRepository,
                           IUserInformationRepository userInformationRepository,
                           IConfiguration config,
                           IMailService mailService,
                           IHomeRepository homeRepository)
        {
            _informationRepository = informationRepository;
            _userRepository = userRepository;
            _userInformationRepository = userInformationRepository;
            _config = config;
            _mailService = mailService;
            _homeRepository = homeRepository;
        }
        public async Task CreateNewHomeAsync(UserModel user, HomeModel home)
        {
            if(user.Position != 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Has Home", "User has home already");
                errors.Throw();
            }

            var isHomeExist = await _homeRepository.GetByHomeNameAsync(home.Name);

            if(isHomeExist != null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Name Exist", "Home name already exist");
                errors.Throw();
            }
            
            user.Position = 2;

            home.Admin = user;
            home.Users = new List<UserModel>();
            home.Users.Add(user);
            _homeRepository.Insert(home);

            user.Home = home;
            _userRepository.Update(user);
        }
    }
}
