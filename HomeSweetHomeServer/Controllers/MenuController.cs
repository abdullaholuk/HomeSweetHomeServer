using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Services;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using HomeSweetHomeServer.Exceptions;
using Newtonsoft.Json;
using System.Web;
using System.Net;

namespace HomeSweetHomeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Menu")]
    [Authorize]
    public class MenuController : Controller
    {
        IJwtTokenService _jwtTokenService;
        IMenuService _menuService;

        public MenuController(IJwtTokenService jwtTokenService, IMenuService menuService)
        {
            _jwtTokenService = jwtTokenService;
            _menuService = menuService;
        }

        //User adds menu
        [HttpPost("AddMenu", Name = "AddMenu")]
        public async Task<IActionResult> AddMenu([FromBody]ClientMenuModel clientMenu)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStrAsync(token);

            MenuModel menu = clientMenu.Menu;
            List<int> mealIds = clientMenu.MealIds;

            await _menuService.AddMenu(user, menu, mealIds);

            return Ok();
        }

        //User adds meal
        [HttpPost("AddMeal", Name = "AddMeal")]
        public async Task<IActionResult> AddMeal([FromBody]MealModel meal)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStrAsync(token);

            meal.Name = meal.Name.ToLower();

            await _menuService.AddMeal(user, meal);

            return Ok();
        }
        
        //User updates meal
        [HttpPost("UpdateMeal", Name = "UpdateMeal")]
        public async Task<IActionResult> UpdateMeal([FromBody]MealModel meal)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStrAsync(token);

            meal.Name = meal.Name.ToLower();

            await _menuService.UpdateMeal(user, meal);

            return Ok();
        }

        //User delketes meal
        [HttpGet("DeleteMeal", Name = "DeleteMeal")]
        public async Task<IActionResult> DeleteMeal([FromQuery]int mealId)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStrAsync(token);

            await _menuService.DeleteMeal(user, mealId);

            return Ok();
        }
    }
}
