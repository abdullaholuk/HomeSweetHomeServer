using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Services
{
    public interface IFCMService
    {
        Task SendNotificationAsync(UserModel user, object notificationSetting);
    }
}
