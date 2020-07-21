using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Reliance.Web.ThisApp.Services.ActionFilters
{
    public class RequirePersonalAccessToken : Attribute //IUpdateSwagger, IAddCorsHeaders
    {
        public const string PersonalAccessToken = "personal-access-token";
        public string[] AdditionalCorsHeaders => new[] { PersonalAccessToken };

        public bool VerifyHeaders(HttpActionContext context)
        {
            if (RequirePersonalAccessTokenAttribute.IsPresent(context.ActionDescriptor) || context.Request.Method == HttpMethod.Options)
                return true;

            return context.ConfirmKeyExistsInHeader(PersonalAccessToken, loggingProperty: "CompanyId");
        }


    }

    public class RequirePersonalAccessTokenAttribute : Attribute
    {
        public static bool IsPresent(HttpActionDescriptor actionDescriptor)
        {
            return
                actionDescriptor.GetCustomAttributes<RequirePersonalAccessTokenAttribute>().Any() ||
                actionDescriptor.ControllerDescriptor.GetCustomAttributes<RequirePersonalAccessTokenAttribute>().Any();
        }
    }
}
