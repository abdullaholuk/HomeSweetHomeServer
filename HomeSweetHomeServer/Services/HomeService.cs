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
        public IFCMService _fcmService;
        public IFriendshipRepository _friendshipRepository;
        public IShoppingListRepository _shoppingListRepository;

        public HomeService(IInformationRepository informationRepository,
                           IUserRepository userRepository,
                           IUserInformationRepository userInformationRepository,
                           IConfiguration config,
                           IMailService mailService,
                           IHomeRepository homeRepository,
                           IFCMService fcmService,
                           IFriendshipRepository friendshipRepository,
                           IShoppingListRepository shoppingListRepository)
        {
            _informationRepository = informationRepository;
            _userRepository = userRepository;
            _userInformationRepository = userInformationRepository;
            _config = config;
            _mailService = mailService;
            _homeRepository = homeRepository;
            _fcmService = fcmService;
            _friendshipRepository = friendshipRepository;
            _shoppingListRepository = shoppingListRepository;
        }

        //Admin creates the home
        public async Task CreateNewHomeAsync(UserModel user, HomeModel home)
        {
            if(user.Position != (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Has Home", "User has home already");
                errors.Throw();
            }

            var isHomeExist = await _homeRepository.GetByHomeNameAsync(home.Name);

            if(isHomeExist != null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Name Exist", "Home name has already exist");
                errors.Throw();
            }
            
            user.Position = (int)UserPosition.Admin;

            home.Admin = user;
            home.Users = new List<UserModel>();
            home.Users.Add(user);
            _homeRepository.Insert(home);

            user.Home = home;
            _userRepository.Update(user);

            _shoppingListRepository.Insert(new ShoppingListModel(home));

        }

        //User requests to admin for joining home
        public async Task JoinHomeRequestAsync(UserModel user, string joinHomeName)
        {
            Task<InformationModel> firstNameInfo = _informationRepository.GetInformationByInformationNameAsync("FirstName");
            Task<InformationModel> lastNameInfo = _informationRepository.GetInformationByInformationNameAsync("LastName");

            if (user.Position != (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Has Home", "User has home already");
                errors.Throw();
            }

            var home = await _homeRepository.GetByHomeNameAsync(joinHomeName, true);

            if (home == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Name Not Exist", "Home name has not exist");
                errors.Throw();
            }

            //User waiting for admin's accept

            UserInformationModel firstName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await firstNameInfo).Id);
            UserInformationModel lastName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await lastNameInfo).Id);
            
            FCMModel fcm = new FCMModel(home.Admin.DeviceId, new Dictionary<string, object>(), "JoinHomeRequest");
            
            fcm.notification.Add("title", "Home Join Request");
            fcm.notification.Add("body", String.Format("{0} {1} ({2}) requests to join your home", firstName.Value, lastName.Value, user.Username));

            fcm.data.Add("RequesterId", user.Id);
            fcm.data.Add("RequesterUsername", user.Username);
            fcm.data.Add("RequesterName", firstName.Value);
            fcm.data.Add("RequesterLastName", lastName.Value);

            await _fcmService.SendFCMAsync(fcm);
        }

        //Admin accepts or rejects user's request
        public async Task JoinHomeAcceptAsync(UserModel user, int requesterId, bool isAccepted)
        {
            Task<UserModel> getAdmin = _userRepository.GetByIdAsync(user.Id, true);
            Task<InformationModel> firstNameInfo = _informationRepository.GetInformationByInformationNameAsync("FirstName");
            Task<InformationModel> lastNameInfo = _informationRepository.GetInformationByInformationNameAsync("LastName");
           
            if (user.Position != (int)UserPosition.Admin)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Authorisation Constraint", "You are not authorized for this request, you must be administrator of home");
                errors.Throw();
            }
            
            UserModel requester = await _userRepository.GetByIdAsync(requesterId);

            if (requester == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Invalid User Id", "User not exist");
                errors.Throw();
            }
            
            if(requester.Position != (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Requester Has Home", "Requester has already home");
                errors.Throw();
            }

            user = await getAdmin;

            if (isAccepted == true)
            {
                requester.Position = (int)UserPosition.HasHome;

                UserInformationModel requesterFirstName = await _userInformationRepository.GetUserInformationByIdAsync(requester.Id, (await firstNameInfo).Id);
                UserInformationModel requesterLastName = await _userInformationRepository.GetUserInformationByIdAsync(requester.Id, (await lastNameInfo).Id);

                HomeModel home = user.Home;

                FCMModel fcmRequester = new FCMModel(requester.DeviceId, new Dictionary<string, object>(), "AllFriends");
                fcmRequester.notification.Add("title", "Join Home Request");
                fcmRequester.notification.Add("body", "Your join home request is accepted by home admin");

                List<UserBaseModel> friendsBaseModels = new List<UserBaseModel>();
                UserBaseModel requesterBaseModel = new UserBaseModel(requester.Id, requester.Username, requester.Position, requesterFirstName.Value, requesterLastName.Value, 0);

                foreach (var friend in home.Users)
                {
                    FriendshipModel friendship = new FriendshipModel(requester, friend, 0);
                    Task insertFriendship = _friendshipRepository.InsertAsync(friendship);

                    //Sends notification to all friends 
                    FCMModel fcmFriend = new FCMModel(friend.DeviceId, new Dictionary<string, object>(), "NewFriend");

                    fcmFriend.notification.Add("title", "New Home Friend");
                    fcmFriend.notification.Add("body", String.Format("{0} {1} ({2}) ", requesterFirstName.Value, requesterLastName.Value, requester.Username));

                    fcmFriend.data.Add("Friend", requesterBaseModel);

                    await _fcmService.SendFCMAsync(fcmFriend);

                    //Sends all friends to requester
                    UserInformationModel friendFirstName = await _userInformationRepository.GetUserInformationByIdAsync(friend.Id, (await firstNameInfo).Id);
                    UserInformationModel friendLastName = await _userInformationRepository.GetUserInformationByIdAsync(friend.Id, (await lastNameInfo).Id);

                    friendsBaseModels.Add(new UserBaseModel(friend.Id, friend.Username, friend.Position, friendFirstName.Value, friendLastName.Value, 0));

                    await insertFriendship;
                }

                home.Users.Add(requester);
                _homeRepository.Update(home);

                requester.Home = home;
                _userRepository.Update(requester);

                fcmRequester.data.Add("NumberOfFriends", home.Users.Count - 1);
                fcmRequester.data.Add("Friends", friendsBaseModels);
                await _fcmService.SendFCMAsync(fcmRequester);
            }
        }

        //Admin request to user for inviting home
        public async Task InviteHomeRequestAsync(UserModel user, string invitedUsername)
        {
            Task<InformationModel> firstNameInfo = _informationRepository.GetInformationByInformationNameAsync("FirstName");
            Task<InformationModel> lastNameInfo = _informationRepository.GetInformationByInformationNameAsync("LastName");

            if (user.Position != (int)UserPosition.Admin)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Authorisation Constraint", "You are not authorized for this request, you must be administrator of home");
                errors.Throw();
            }

            var home = (await _userRepository.GetByIdAsync(user.Id, true)).Home;

            //Admin waiting for user's accept

            UserModel invitedUser = await _userRepository.GetByUsernameAsync(invitedUsername);

            if(invitedUser == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Not Exist", "User is not exist");
                errors.Throw();
            }

            if(invitedUser.Position != (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Has Home", "You can not invite a user who already has home");
                errors.Throw();
            }

            UserInformationModel firstName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await firstNameInfo).Id);
            UserInformationModel lastName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await lastNameInfo).Id);

            FCMModel fcm = new FCMModel(invitedUser.DeviceId, new Dictionary<string, object>(), "InviteHomeRequest");

            fcm.notification.Add("title", "Home Invite Request");
            fcm.notification.Add("body", String.Format("{0} {1} ({2}) invites to join his/her home", firstName.Value, lastName.Value, user.Username));

            fcm.data.Add("InvitedHomeId", home.Id);
            fcm.data.Add("InviterUsername", user.Username);
            fcm.data.Add("InviterFirstName", firstName.Value);
            fcm.data.Add("InviterLastName", lastName.Value);

            await _fcmService.SendFCMAsync(fcm);
        }

        //User accepts or rejects admin's request
        public async Task InviteHomeAcceptAsync(UserModel user, int invitedHomeId, bool isAccepted)
        {
            Task<InformationModel> firstNameInfo = _informationRepository.GetInformationByInformationNameAsync("FirstName");
            Task<InformationModel> lastNameInfo = _informationRepository.GetInformationByInformationNameAsync("LastName");

            if (user.Position != (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Has Home", "User can not accept invite requests while already has home");
                errors.Throw();
            }

            HomeModel home = await _homeRepository.GetByIdAsync(invitedHomeId, true);

            if (home == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Invalid Home Id", "Home not exist");
                errors.Throw();
            }

            if (isAccepted == true)
            {
                user.Position = (int)UserPosition.HasHome;

                UserInformationModel userFirstName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await firstNameInfo).Id);
                UserInformationModel userLastName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await lastNameInfo).Id);

                List<UserBaseModel> friendsBaseModels = new List<UserBaseModel>();
                UserBaseModel userBaseModel = new UserBaseModel(user.Id, user.Username, user.Position, userFirstName.Value, userLastName.Value, 0);

                FCMModel fcmUser = new FCMModel(user.DeviceId, type: "AllFriends");

                foreach (var friend in home.Users)
                {
                    FriendshipModel friendship = new FriendshipModel(user, friend, 0);
                    Task insertFriendship = _friendshipRepository.InsertAsync(friendship);
                    
                    //Sends notification to all friends 
                    FCMModel fcmFriend = new FCMModel(friend.DeviceId, new Dictionary<string, object>(), "NewFriend");

                    fcmFriend.notification.Add("title", "New Home Friend");
                    fcmFriend.notification.Add("body", String.Format("{0} {1} ({2}) ", userFirstName.Value, userLastName.Value, user.Username));

                    fcmFriend.data.Add("Friend", userBaseModel);
                    
                    await _fcmService.SendFCMAsync(fcmFriend);

                    //Sends all friends to requester
                    UserInformationModel friendFirstName = await _userInformationRepository.GetUserInformationByIdAsync(friend.Id, (await firstNameInfo).Id);
                    UserInformationModel friendLastName = await _userInformationRepository.GetUserInformationByIdAsync(friend.Id, (await lastNameInfo).Id);
    
                    friendsBaseModels.Add(new UserBaseModel(friend.Id, friend.Username, friend.Position, friendFirstName.Value, friendLastName.Value, 0));

                    await insertFriendship;
                }

                home.Users.Add(user);
                _homeRepository.Update(home);

                user.Home = home;
                _userRepository.Update(user);

                fcmUser.data.Add("NumberOfFriends", home.Users.Count - 1);
                fcmUser.data.Add("Friends", friendsBaseModels);
                await _fcmService.SendFCMAsync(fcmUser);
            }
        }

        //User gives money to his/her friend
        public async Task TransferMoneyToFriendAsync(UserModel from, UserModel to, double givenMoney)
        { 
            if (to == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Friend Not Found", "Friend not found for lend");
                errors.Throw();
            }

            if (from.Position == (int)UserPosition.HasNotHome || to.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            FriendshipModel friendship = await _friendshipRepository.GetFriendshipByIdAsync(from.Id, to.Id);
            if(friendship == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Friendship Not Found", "Friendship not found for lend");
                errors.Throw();
            }

            if(friendship.User1.Id == from.Id)
            {
                friendship.Debt -= givenMoney;
                Task update = _friendshipRepository.UpdateAsync(friendship);

                FCMModel fcmFrom = new FCMModel(from.DeviceId, type: "GiveMoney");
                fcmFrom.data.Add("ToId", to.Id);
                fcmFrom.data.Add("NewDebt", friendship.Debt);
                Task sendFCMFrom = _fcmService.SendFCMAsync(fcmFrom);

                FCMModel fcmTo = new FCMModel(to.DeviceId, type: "TakeMoney");
                fcmTo.data.Add("FromId", from.Id);
                fcmTo.data.Add("NewDebt", -friendship.Debt);

                await _fcmService.SendFCMAsync(fcmTo);
                await sendFCMFrom;
                await update;
            }
            else
            {
                friendship.Debt += givenMoney;
                Task update = _friendshipRepository.UpdateAsync(friendship);

                FCMModel fcmFrom = new FCMModel(from.DeviceId, type: "GiveMoney");
                fcmFrom.data.Add("ToId", to.Id);
                fcmFrom.data.Add("NewDebt", -friendship.Debt);
                Task sendFCMFrom = _fcmService.SendFCMAsync(fcmFrom);

                FCMModel fcmTo = new FCMModel(to.DeviceId, type: "TakeMoney");
                fcmTo.data.Add("FromId", from.Id);
                fcmTo.data.Add("NewDebt", friendship.Debt);

                await _fcmService.SendFCMAsync(fcmTo);
                await sendFCMFrom;
                await update;
            }
        }
    }
}
