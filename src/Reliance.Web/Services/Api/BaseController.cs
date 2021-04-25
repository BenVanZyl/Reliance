using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowStorm.QueryExecutors;

namespace Reliance.Web.Services.Api
{
    public class BaseController : Controller
    {
        public ILogger<object> Logger { get; set; }
        public IQueryExecutor Executor { get; set; }
        public IMediator Mediator { get; set; }

        public BaseController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator)
        {
            Logger = logger;
            Executor = executor;
            Mediator = mediator;
        }
    }
}
