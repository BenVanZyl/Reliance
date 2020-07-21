using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Reliance.Web.ThisApp.Services.ActionFilters
{
    public static class HttpContextExtensions
    {
        public static bool ConfirmKeyExistsInHeader(this HttpActionContext context, string key, string loggingProperty, string fallbackValue = null)
        {
            string value;
            if (!context.Request.GetValueFromHeader(key, out value, fallbackValue))
            {
                context.Response = context.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"'{key}' is required in the request header");
                return false;
            }

            //TODO: Setup logging!

            return true;
        }

        public static bool GetValueFromHeader(this HttpRequestMessage request, string key, out string value, string fallbackValue = null)
        {
            IEnumerable<string> values;
            request.Headers.TryGetValues(key, out values);
            value = values?.FirstOrDefault();
            value = value ?? fallbackValue;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return true;
        }

    }
}
