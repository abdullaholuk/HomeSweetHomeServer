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

namespace HomeSweetHomeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    
    public class AuthenticationController : Controller
    {
        IAuthenticationService _authenticationService;
        IJwtTokenService _jwtTokenService;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenService jwtTokenService)
        {
            _authenticationService = authenticationService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("Register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel registrationForm)
        {
            registrationForm.RegistrationDate = DateTime.UtcNow;

            await _authenticationService.ControlRegisterFormAsync(registrationForm);
            await _authenticationService.RegisterNewUser(registrationForm);

            return Ok();
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            AuthenticationModel user = await _authenticationService.GetUser(login);
            string token = _jwtTokenService.CreateToken(user);
            /*if (user.IsVerifiedByEmail == false)
            {

            }*/
            
            return Ok(token);
        }
        /*
        // GET: api/Authentication
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authentication/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Authentication
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Authentication/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
