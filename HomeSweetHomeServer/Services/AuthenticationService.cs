using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Repositories;
using HomeSweetHomeServer.Services;

namespace HomeSweetHomeServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public IRepository<AuthenticationModel> _authenticationRepository;
        public IRepository<InformationModel> _informationRepository;
        public IRepository<UserInformationModel> _userInformationRepository;

        public AuthenticationService(IRepository<AuthenticationModel> authenticationRepository,
            IRepository<InformationModel> informationRepository,
            IRepository<UserInformationModel> userInformationRepository)
        {
            _authenticationRepository = authenticationRepository;
            _informationRepository = informationRepository;
            _userInformationRepository = userInformationRepository;
        }

        public async Task ControlRegisterFormAsync(RegistrationModel RegistrationForm)
        {

            return ;
        }

       /* public async Task<UserModel> RegisterNewUser(RegistrationModel RegistirationForm)
        {

        }*/
    }
}
