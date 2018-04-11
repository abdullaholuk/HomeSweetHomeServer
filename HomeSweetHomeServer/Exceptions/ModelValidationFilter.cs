using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeSweetHomeServer.Exceptions
{
    //Custom model validation filter
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            if(!Context.ModelState.IsValid)
            {
                
                object Errors = new BadRequestObjectResult(Context.ModelState).Value;
                string Message = JsonConvert.SerializeObject(Errors);

                throw new CustomException(Message, (int) HttpStatusCode.BadRequest);
            }
        }
    }
}
