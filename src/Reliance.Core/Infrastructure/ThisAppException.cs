using System;
using System.Net;

namespace Reliance.Core.Services.Infrastructure
{
    public class ThisAppException : Exception
    {
        public int StatusCode { get; set; }

        private readonly string _message;
        public override string Message => string.IsNullOrEmpty(_message) ? base.Message : _message;

        public ThisAppException(int statusCode, string message)
        {
            StatusCode = statusCode;
            _message = message;
        }

        public ThisAppException(HttpStatusCode statusCode, string message)
        {
            //StatusCode = (long)statusCode.ToString();
            StatusCode = 500;
            _message = message;
        }
    }
}
