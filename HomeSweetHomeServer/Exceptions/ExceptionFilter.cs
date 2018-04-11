using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace HomeSweetHomeServer.Exceptions
{
    //Catches exceptions which generated custom or automatically and responses to client
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext Context)
        {
            string Message;
            int StatusCode;

            Exception _exception = Context.Exception;

            if (_exception is CustomException) //Generated exception about any error
            {
                var Exception = (CustomException)_exception;
                Message = Exception.Message;
                StatusCode = Exception.StatusCode;
            } else //Generated about unhandled system exception
            {
                Message = _exception.Message;
                StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            Context.ExceptionHandled = true;
            
            string MessageJson = JsonConvert.SerializeObject(new Dictionary<string, string> { { "Message", Message } }, Formatting.Indented);
            
            //Response creation
            HttpResponse Response = Context.HttpContext.Response;
            Response.StatusCode = StatusCode;
            Response.ContentType = "application/json";
            Response.WriteAsync(MessageJson);
        }
    }
}
