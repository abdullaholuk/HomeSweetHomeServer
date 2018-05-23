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
    public class UserExpenseService : IUserExpenseService
    {
        IExpenseRepository _expenseRepository;
        IUserExpenseRepository _userExpenseRepository;
        IFCMService _fcmService;
        IHomeService _homeService;
        IUserRepository _userRepository;
        IInformationRepository _informationRepository;
        IUserInformationRepository _userInformationRepository;
        IHomeRepository _homeRepository;

        public UserExpenseService(IExpenseRepository expenseRepository,
                                  IUserExpenseRepository userExpenseRepository,
                                  IFCMService fcmService,
                                  IHomeService homeService,
                                  IUserRepository userRepository,
                                  IInformationRepository informationRepository,
                                  IUserInformationRepository userInformationRepository,
                                  IHomeRepository homeRepository)
        {
            _expenseRepository = expenseRepository;
            _userExpenseRepository = userExpenseRepository;
            _fcmService = fcmService;
            _homeService = homeService;
            _userRepository = userRepository;
            _informationRepository = informationRepository;
            _userInformationRepository = userInformationRepository;
            _homeRepository = homeRepository;
        }

        //User adds expense
        public async Task AddExpenseAsync(UserModel user, ExpenseModel expense, List<int> participants)
        {
            if(user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            if ((expense.EType > (int)ExpenseType.Others) || (expense.EType < 0) || (expense.EType == (int)ExpenseType.Borrow))
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Expense Type Not Exist", "Expense type number not valid");
                errors.Throw();
            }

            if (expense.Cost <= 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Expense Cost Not Valid", "The expense cost must be bigger than 0");
                errors.Throw();
            }

            participants = participants.Distinct().ToList();
            
            if (participants.Count == 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Participants Not Exist", "You must add participants for this expense");
                errors.Throw();
            }

            Task<InformationModel> firstNameInfo = _informationRepository.GetInformationByInformationNameAsync("FirstName");
            Task<InformationModel> lastNameInfo = _informationRepository.GetInformationByInformationNameAsync("LastName");

            user = await _userRepository.GetByIdAsync(user.Id, true);
            HomeModel home = await _homeRepository.GetByIdAsync(user.Home.Id, true);
            
            //Friend not found
            foreach (var p in participants)
            {
                if (home.Users.SingleOrDefault(u => u.Id == p) == null)
                {
                    CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                    errors.AddError("Friendship Not Found", "Friendship not found for lend");
                    errors.Throw();
                }
            }

            user = await _userRepository.GetByIdAsync(user.Id);

            expense.LastUpdated = DateTime.UtcNow;
            expense.Home = home;
            expense.Author = await _userRepository.GetByIdAsync(user.Id); ;

            //Author informations
            UserInformationModel userFirstName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await firstNameInfo).Id);
            UserInformationModel userLastName = await _userInformationRepository.GetUserInformationByIdAsync(user.Id, (await lastNameInfo).Id);

            //Lend
            if (expense.EType == (int)ExpenseType.Lend)
            {
                
                if(participants.SingleOrDefault(p => p.Equals(user.Id)) != 0)
                {
                    CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                    errors.AddError("Lend Error", "You cant lend yourself");
                    errors.Throw();
                }

                ExpenseModel borrowExpense = new ExpenseModel((int)ExpenseType.Borrow,
                                                                  expense.Cost / participants.Count,
                                                                  expense.Author,
                                                                  expense.LastUpdated,
                                                                  expense.Title,
                                                                  expense.Content);
                
                _expenseRepository.Insert(borrowExpense);
                _expenseRepository.Insert(expense);

                //Borrow for all participants
                foreach (var p in participants)
                { 
                    var to = await _userRepository.GetByIdAsync(p);

                    await _homeService.TransferMoneyToFriendAsync(user, to, borrowExpense.Cost);
                    await _userExpenseRepository.InsertAsync(new UserExpenseModel(to, borrowExpense));

                    //Send fcm to other participants
                    FCMModel fcmBorrow = new FCMModel(to.DeviceId, new Dictionary<string, object>(), "Expense");
                    fcmBorrow.notification.Add("title", "Borç Para");
                    fcmBorrow.notification.Add("body", String.Format("{{0} {1} ({2}) kişisinden {3:c} borç alındı.",
                                                                                                    userFirstName.Value,
                                                                                                    userLastName.Value,
                                                                                                    user.Username,
                                                                                                    borrowExpense.Cost));
                    fcmBorrow.data.Add("Content", borrowExpense);
                    await _fcmService.SendFCMAsync(fcmBorrow);
                }
                
                Task insertUEL = _userExpenseRepository.InsertAsync(new UserExpenseModel(user, expense));

                //Send fcm to user
                FCMModel fcmLend = new FCMModel(user.DeviceId, type : "Expense");
                fcmLend.data.Add("Content", expense);
                await _fcmService.SendFCMAsync(fcmLend);

                await insertUEL;
            }

            else
            {
                expense.Cost = expense.Cost / participants.Count;
                _expenseRepository.Insert(expense);

                foreach (var p in participants)
                {
                    //Paid for other friends
                    if (p != user.Id)
                    {
                        var to = await _userRepository.GetByIdAsync(p);

                        await _homeService.TransferMoneyToFriendAsync(user, to, expense.Cost);
                        await _userExpenseRepository.InsertAsync(new UserExpenseModel(to, expense));

                        //Send fcm to other participants
                        FCMModel fcmExpense = new FCMModel(to.DeviceId, new Dictionary<string, object>(), "Expense");
                        fcmExpense.notification.Add("title", String.Format("Yeni Gider : \"{0}\"", expense.Title));
                        fcmExpense.notification.Add("body", String.Format("{0} {1} ({2}) kişisi tarafından {3:c} ödendi.", 
                                                                                                      userFirstName.Value,
                                                                                                      userLastName.Value,
                                                                                                      user.Username,
                                                                                                      expense.Cost));
                        fcmExpense.data.Add("Content", expense);
                        await _fcmService.SendFCMAsync(fcmExpense);
                    }
                    else
                    {
                        Task insertUE = _userExpenseRepository.InsertAsync(new UserExpenseModel(user, expense));

                        //Send fcm to user
                        FCMModel fcmExpense = new FCMModel(user.DeviceId, new Dictionary<string, object>(), "Expense");
                        fcmExpense.data.Add("Content", expense);
                        await _fcmService.SendFCMAsync(fcmExpense);

                        await insertUE;
                    }
                }
            }
        }
    }
}
