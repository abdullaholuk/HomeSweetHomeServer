using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Services
{
    //Interface about home operations
    public interface IHomeService
    {
        Task CreateNewHomeAsync(UserModel user, HomeModel home);
        Task JoinHomeRequestAsync(UserModel user, string joinHomeName);
        Task JoinHomeAcceptAsync(UserModel user, int requesterId, bool isAccepted);
        Task InviteHomeRequestAsync(UserModel user, string invitedUsername);
        Task InviteHomeAcceptAsync(UserModel user, int invitedHomeId, bool isAccepted);
        Task GiveMoneyToFriendAsync(UserModel from, UserModel to, double givenMoney);
    }
}
