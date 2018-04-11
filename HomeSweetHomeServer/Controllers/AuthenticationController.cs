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

namespace HomeSweetHomeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    
    public class AuthenticationController : Controller
    {
        IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel RegistrationForm)
        {
            RegistrationForm.RegistirationDate = DateTime.UtcNow;
            await _authenticationService.ControlRegisterFormAsync(RegistrationForm);
            
            return Ok();
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
