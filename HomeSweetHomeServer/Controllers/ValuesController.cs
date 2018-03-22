using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Repositories;
using HomeSweetHomeServer.Services;


namespace HomeSweetHomeServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IRepository<InformationModel> _repository;

        public ValuesController(IRepository<InformationModel> repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            /*    AuthenticationModel user = new AuthenticationModel();
              //  user.UserId = 1;
                user.Username = "apo";
                user.Password = "12345";
                user.Token = "adsadas";
                _repository.addUser(user);
                */
            InformationModel info = new InformationModel();
            info.InformationId = 1;
            info.InformationName = "isim";
            info.InformationType = "str";
            _repository.addUser(info);

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
