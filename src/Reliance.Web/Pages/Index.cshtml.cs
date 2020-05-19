using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Reliance.Web.Client.Api;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.QueryExecutors;

namespace Reliance.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IQueryExecutor _executor;
        private readonly IMediator _mediator;

        #region "Model Properties"
        public string SnowStormGreeting { get; set; }

        public List<PackageDto> Packages { get; set; }

        #endregion
        
        public IndexModel(ILogger<IndexModel> logger, IQueryExecutor executor, IMediator mediator)
        {
            _logger = logger;
            _executor = executor;
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            //todo: call static method on SnowStorm component to show "hello world"
            SnowStormGreeting = SnowStorm.Greeting.Message;
            await GetData();
        }

        public async Task GetData()
        {
            if (!User.Identity.IsAuthenticated)
                return;  //nothing to do, user is not authenticated


            Packages = await _executor.WithMapping<PackageDto>().ExecuteAsync(new GetPackagesQuery(), o => o.Name);

        }
    }
}
