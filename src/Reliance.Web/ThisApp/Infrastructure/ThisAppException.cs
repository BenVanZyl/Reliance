using System;

namespace Reliance.Web.ThisApp.Infrastructure
{
    public class ThisAppException : Exception
    {
        public int StatusCode { get; set; }

        private string _message;
        public override string Message => string.IsNullOrEmpty(_message) ? base.Message : _message;

        public ThisAppException(int statusCode, string message)
        {
            StatusCode = statusCode;
            _message = message;
        }
    }
}
