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
    public class MenuService : IMenuService
    {
        IMealRepository _mealRepository;
        IMenuRepository _menuRepository;
        IMenuMealRepository _menuMealRepository;
        IUserRepository _userRepository;
        IHomeRepository _homeRepository;
        IFCMService _fcmService;

        public MenuService(IMenuRepository menuRepository,
                           IMealRepository mealRepository,
                           IMenuMealRepository menuMealRepository,
                           IHomeRepository homeRepository,
                           IUserRepository userRepository,
                           IFCMService fcmService)
        {
            _mealRepository = mealRepository;
            _menuRepository = menuRepository;
            _menuMealRepository = menuMealRepository;
            _userRepository = userRepository;
            _homeRepository = homeRepository;
            _fcmService = fcmService;
        }

        //User synchronize home menu meals
        public async Task<List<ClientMenuModel>> SynchronizeMenusAsync(UserModel user)
        {
            if (user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);
            List<MenuModel> homeMenus = await _menuRepository.GetAllHomeMenusAsync(user.Home.Id);
            List<ClientMenuModel> clientMenus = new List<ClientMenuModel>();
             
            foreach(var menu in homeMenus)
            {
                ClientMenuModel clientMenu = new ClientMenuModel();
                clientMenu.Menu = menu;

                List<MenuMealModel> menuMeals = await _menuMealRepository.GetAllMenuMealsByMenuIdAsync(menu.Id, true);

                foreach (var menuMeal in menuMeals)
                    clientMenu.MealIds.Add(menuMeal.Meal.Id);

                clientMenus.Add(clientMenu);
            }

            return clientMenus;
        }

        //User adds menu
        public async Task AddMenuAsync(UserModel user, MenuModel menu, List<int> mealIds)
        {
            if (user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            if (menu.Date.Kind != DateTimeKind.Utc)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Date Not Valid", "Date is not valid, please convert to UTC");
                errors.Throw();
            }

            menu.Date = menu.Date.AddHours(-menu.Date.Hour);
            menu.Date = menu.Date.AddMinutes(-menu.Date.Minute);
            menu.Date = menu.Date.AddSeconds(-menu.Date.Second);
            menu.Date = menu.Date.AddMilliseconds(-menu.Date.Millisecond);

            user = await _userRepository.GetByIdAsync(user.Id, true);
            HomeModel home = await _homeRepository.GetByIdAsync(user.Home.Id, true);

            MenuModel tmp = await _menuRepository.GetHomeMenuByDateAsync(home.Id, menu.Date);
            
            if(tmp != null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Date Not Valid", "Date is not valid, that day has already menu");
                errors.Throw();
            }

            mealIds = mealIds.Distinct().ToList();

            if(mealIds.Count == 0)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Menu Has Not Meal", "There is no meals for this menu");
                errors.Throw();
            }
            
            menu.Home = home;
            
            //Finds meals that are not related home
            foreach(var mealId in mealIds)
            {
                MealModel meal = await _mealRepository.GetHomeMealByIdAsync(mealId, true);

                if ((meal == null) || (meal.Home.Id != home.Id))
                {
                    CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                    errors.AddError("Meal Not Related Home", "This meal is not related with user home");
                    errors.Throw();
                }

            }
            
            _menuRepository.Insert(menu);

            //Inserts menu meal to database
            foreach (var mealId in mealIds)
            {
                MealModel meal = await _mealRepository.GetHomeMealByIdAsync(mealId, true);

                MenuMealModel menuMeal = new MenuMealModel(menu, meal);

                _menuMealRepository.Insert(menuMeal);
            }

            //Sends fcm to all users
            foreach(var friend in home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type: "NewMenu");
                fcm.data.Add("Menu", menu);
                fcm.data.Add("MealIds", mealIds);

                await _fcmService.SendFCMAsync(fcm);

            }
        }

        //User synchronizes home meals
        public async Task<List<MealModel>> SynchronizeMealsAsync(UserModel user)
        {
            if (user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);

            return await _mealRepository.GetAllHomeMealsAsync(user.Home.Id);
        }
        

        //User adds meal
        public async Task AddMealAsync(UserModel user, MealModel meal)
        {
            if (user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);
            HomeModel home = await _homeRepository.GetByIdAsync(user.Home.Id, true);

            MealModel tmp = await _mealRepository.GetHomeMealByNameAsync(home.Id, meal.Name);

            if(tmp != null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Meal Already Exist", "This meal has already exist");
                errors.Throw();
            }

            meal.Home = home;

            _mealRepository.Insert(meal);

            foreach(var friend in home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type : "AddMeal");
                fcm.data.Add("Meal", meal);
                await _fcmService.SendFCMAsync(fcm);
            }
        }

        //User updates meal
        public async Task UpdateMealAsync(UserModel user, MealModel meal)
        {
            if (user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);
            HomeModel home = await _homeRepository.GetByIdAsync(user.Home.Id, true);

            MealModel old = await _mealRepository.GetHomeMealByIdAsync(meal.Id, true);

            if (old == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Meal Not Exist", "This meal has not exist");
                errors.Throw();
            }

            if(old.Home.Id != home.Id)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Meal Not Related Home", "This meal is not related with user home");
                errors.Throw();
            }

            old.Name = meal.Name;
            old.Ingredients = meal.Ingredients;
            old.Note = meal.Note;

            _mealRepository.Update(old);

            foreach (var friend in home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type: "UpdateMeal");
                fcm.data.Add("Meal", meal);
                await _fcmService.SendFCMAsync(fcm);
            }
        }

        //User deletes meal
        public async Task DeleteMealAsync(UserModel user, int mealId)
        {
            if (user.Position == (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Home Not Exist", "User is not member of a home");
                errors.Throw();
            }

            user = await _userRepository.GetByIdAsync(user.Id, true);
            HomeModel home = await _homeRepository.GetByIdAsync(user.Home.Id, true);

            MealModel meal = await _mealRepository.GetHomeMealByIdAsync(mealId, true);

            if (meal == null)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Meal Not Exist", "This meal has not exist");
                errors.Throw();
            }

            if (meal.Home.Id != home.Id)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Meal Not Related Home", "This meal is not related with user home");
                errors.Throw();
            }

            foreach (var friend in home.Users)
            {
                FCMModel fcm = new FCMModel(friend.DeviceId, type: "DeleteMeal");
                fcm.data.Add("MealId", mealId);
                await _fcmService.SendFCMAsync(fcm);
            }

            _mealRepository.Delete(meal);            
        }
    }
}
