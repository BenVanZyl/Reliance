using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto;
using Reliance.Web.Client.Dto.Dashboard;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Commands.DevOps;
using Reliance.Web.ThisApp.Services.Queries.DevOps;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Api.DevOps
{
    public class DashboardController : BaseController
    {
        public DashboardController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        // TODO: Implement authorisation
        // TODO: Validate that user requesting info has permission and is associated with organisation
        [HttpGet]
        [Route("api/dashboards/apps/organisations/{organisationid}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<RefTypeDto>>> GetApps(long organisationId)
        {
            try
            {
                Logger.LogInformation($"GetApps was queried by {User.Identity.Name}");
                var values = await Executor.CastTo<RefTypeDto>().Execute(new GetAppsQuery(organisationId), o => o.Name);
                Logger.LogInformation(Request.Path);
                return Ok(values);
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetApps Failed: {ex.Message}", ex);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/dashboards/apps/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<RefTypeDto>>> GetApp(long id)
        {
            try
            {
                Logger.LogInformation($"GetApp was queried by {User.Identity.Name} with Id={id}");
                var values = await Executor.CastTo<RefTypeDto>().Execute(new GetAppQuery(id));
                Logger.LogInformation(Request.Path);
                return Ok(values);
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetApp Failed: {ex.Message}", ex);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        [Route("api/dashboards/stages/organisations/{organisationid}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<RefTypeDto>>> GetStages(long organisationId)
        {
            try
            {
                Logger.LogInformation($"GetStages was queried by {User.Identity.Name}");
                var values = await Executor.CastTo<RefTypeDto>().Execute(new GetStagesQuery(organisationId));
                Logger.LogInformation(Request.Path);
                return Ok(values);
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetStages Failed: {ex.Message}", ex);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/dashboards/stages/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<RefTypeDto>>> GetStage(long id)
        {
            try
            {
                Logger.LogInformation($"GetStages was queried by {User.Identity.Name} with Id={id}");
                var values = await Executor.CastTo<RefTypeDto>().Execute(new GetStageQuery(id));
                Logger.LogInformation(Request.Path);
                return Ok(values);
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetStages Failed: {ex.Message}", ex);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/dashboards/organisations/{organisationid}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<DashboardDto>>> GetDashboards(long organisationId)
        {
            try
            {
                Logger.LogInformation($"GetDashboards()");
                var values = await Mediator.Send(new GetDashboardCommand(organisationId));
                Logger.LogInformation(Request.Path);
                return Ok(values);
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetDashboards Failed: {ex.Message}", ex);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/dashboards/badge")]
        [Produces("application/json")]
        public async Task<ActionResult<List<DashboardDto>>> PostBadge([FromBody] BadgeDto badge)
        {
            try
            {
                Logger.LogInformation($"PostBadge()");
                var values = await Mediator.Send(new SaveBadgeCommand(badge));
                Logger.LogInformation(Request.Path);
                return Ok(values);
            }
            catch (Exception ex)
            {
                Logger.LogError($"PostBadge Failed: {ex.Message}", ex);
                return Ok(HttpStatusCode.InternalServerError);
            }
        }
    }
}
