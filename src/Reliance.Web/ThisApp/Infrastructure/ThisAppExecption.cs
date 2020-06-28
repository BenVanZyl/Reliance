using System;

namespace Reliance.Web.ThisApp.Infrastructure
{
    public class ThisAppExecption : Exception
    {
        public int StatusCode { get; set; }

        private string _message;
        public override string Message => string.IsNullOrEmpty(_message) ? base.Message : _message;

        public ThisAppExecption(int statusCode, string message)
        {
            StatusCode = statusCode;
            _message = message;
        }
    }
}
