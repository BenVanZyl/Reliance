using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowStorm.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Api
{

    //[RequireHttps]
    [AllowAnonymous]
    public class AnonDataController : BaseController
    {
        public AnonDataController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }


        [HttpGet]
        [Route("api/anon-data/ping")]
        //[Something(Request)]
        //[RequirePersonalAccessToken]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetAnonDataPing()
        {
            return Ok("Hello World!");
        }

        [HttpGet]
        [Route("api/anon-data/ping/secure")]
        [RequireHttps]
        //[RequirePersonalAccessToken]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetAnonDataPingSecure()
        {
            return Ok("Hello World!");
        }
    }
}
