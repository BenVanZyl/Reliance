using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowStorm.Infrastructure.QueryExecutors;

namespace Reliance.Web.ThisApp.Api
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
