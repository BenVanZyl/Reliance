using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SnowStorm.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Infrastructure
{
    [RequireHttps]
    public class BasePageModel : PageModel
    {
        public ILogger<object> SiteLogger { get; set; }
        public IQueryExecutor Executor { get; set; }
        public IMediator Mediator { get; set; }
        public BasePageModel(ILogger<object> logger, IQueryExecutor executor, IMediator mediator)
        {
            SiteLogger = logger;
            Executor = executor;
            Mediator = mediator;

        }
    }
}
