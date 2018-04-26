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
    [Route("api/Home")]
    [Authorize]

    public class HomeController : Controller
    {
        IHomeService _homeService;
        IJwtTokenService _jwtTokenService;

        public HomeController(IHomeService homeService, IJwtTokenService jwtTokenService)
        {
            _homeService = homeService;
            _jwtTokenService = jwtTokenService;
        }

        //Creates new home from admin user
        [HttpPost("CreateNewHome", Name = "CreateNewHome")]
        public async Task<IActionResult> CreateNewHome([FromBody]HomeModel home)
        {
            home.Name = home.Name.ToLower();

            string token = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            UserModel user = await _jwtTokenService.GetUserFromTokenStr(token);

            await _homeService.CreateNewHomeAsync(user, home);
            return Ok();
        }
    }
}
