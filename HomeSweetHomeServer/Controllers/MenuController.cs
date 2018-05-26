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
        public async Task<IActionResult> AddMenu(MenuModel menu)
        {
            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStrAsync(token);

            await _menuService.AddMenu(user, menu);

            return Ok();
        }
    }
}
