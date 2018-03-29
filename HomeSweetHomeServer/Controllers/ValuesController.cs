using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using HomeSweetHomeServer.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace HomeSweetHomeServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IRepository<AuthenticationModel> _repository;
        public IJwtTokenService _tokenService;

        public ValuesController(IRepository<AuthenticationModel> repository, IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        //[Authorize]
        public IEnumerable<string> Get()
        {
            AuthenticationModel user = new AuthenticationModel();
            user.Username = "apo";
            user.Password = "12345";
            user.Token = _tokenService.CreateToken(user);
            _repository.addUser(user);
           
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
