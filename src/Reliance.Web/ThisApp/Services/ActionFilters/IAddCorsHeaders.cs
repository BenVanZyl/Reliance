using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.ActionFilters
{
    internal interface IAddCorsHeaders
    {
        string[] AdditionalCorsHeaders { get; }
    }
}
