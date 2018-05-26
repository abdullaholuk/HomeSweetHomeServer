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
        public async Task AddMenu(UserModel user, MenuModel menu)
        {
            if (user.Position != (int)UserPosition.HasNotHome)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("User Has Home", "User has home already");
                errors.Throw();
            }

            if(menu.Date.Kind != DateTimeKind.Utc)
            {
                CustomException errors = new CustomException((int)HttpStatusCode.BadRequest);
                errors.AddError("Date Not Valid", "Date is not valid, please convert to UTC");
                errors.Throw();
            }
        }
    }
}
