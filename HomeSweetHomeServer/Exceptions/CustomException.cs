using System;
using System.Net;

namespace HomeSweetHomeServer.Exceptions
{
    //Custom exceptions will generate for any exception and error
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException(int StatusCode = (int)HttpStatusCode.InternalServerError)
        {
            this.StatusCode = StatusCode;
        }

        public CustomException(string Message, int StatusCode = (int)HttpStatusCode.InternalServerError) : base(Message)
        {
            this.StatusCode = StatusCode;
        }

        public CustomException(string Message, Exception Inner, int StatusCode = (int)HttpStatusCode.InternalServerError) : base(Message, Inner)
        {
            this.StatusCode = StatusCode;
        }
    }
}
