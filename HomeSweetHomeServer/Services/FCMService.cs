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
using System.Net.Http;
using System.Text;
using System.IO;

namespace HomeSweetHomeServer.Services
{
    public class FCMService : IFCMService
    {
        public IInformationRepository _informationRepository;
        public IUserRepository _userRepository;
        public IUserInformationRepository _userInformationRepository;
        public IConfiguration _config;
        public IMailService _mailService;
        public IHomeRepository _homeRepository;

        public FCMService(IInformationRepository informationRepository,
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

        //Send Firebase Cloud Message to given user
        public async Task SendFCMToUserAsync(UserModel user, FCMModel fcmMessage)
        {
            string serverKey = string.Format("key={0}", _config["FCM:ServerKey"]);
            string senderId = string.Format("id={0}", _config["FCM:SenderId"]);
            string requestUri = _config["FCM:RequestUri"];
            string deviceId = user.DeviceId;

            var jsonMessageBody = JsonConvert.SerializeObject(fcmMessage);

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                httpRequest.Content = new StringContent(jsonMessageBody, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        return ;
                    }
                    else
                    {
                        CustomException errors = new CustomException();
                        errors.AddError("FCM Error", "Unexpected error occured while FCM sending");
                        errors.Throw();
                    }
                }
            }
        }
    }
}
