using MediatR;
using Microsoft.AspNetCore.Http;
using Reliance.Web.Client.Dto.Dashboard;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries.DevOps;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Commands.DevOps
{
    public class GetDashboardCommand : IRequest<List<DashboardDto>>
    {
        public long OrgId { get; set; }

        public GetDashboardCommand(long organisationId)
        {
            OrgId = organisationId;
        }
}

    public class GetDashboardCommandHandler : IRequestHandler<GetDashboardCommand, List<DashboardDto>>
    {
        private readonly IQueryExecutor _executor;
        public GetDashboardCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<List<DashboardDto>> Handle(GetDashboardCommand request, CancellationToken cancellationToken)
        {
            var dashboard = new List<DashboardDto>();
            try
            {
                // get apps
                var apps = await _executor.Execute(new GetAppsQuery(request.OrgId));
                if (apps == null || apps.Count == 0)
                    throw new ThisAppException(StatusCodes.Status404NotFound, "No Apps found");

                // get stages
                var stages = await _executor.Execute(new GetStagesQuery(request.OrgId));
                if (stages == null || stages.Count == 0)
                    throw new ThisAppException(StatusCodes.Status404NotFound, "No Stages found");

                // get badge urls
                foreach (var app in apps)
                {
                    var dto = new DashboardDto
                    {
                        AppId = app.Id,
                        AppName = app.Name,
                        Stages = new List<BadgeDto>()
                    };

                    foreach (var stage in stages)
                    {
                        var badge = await _executor.CastTo<BadgeDto>().Execute(new GetBadgeQuery(app.Id, stage.Id));
                        if (badge == null)
                            badge = new BadgeDto
                            {
                                Id = 0,
                                AppId = app.Id,
                                StageId = stage.Id,
                                BadgeUrl = ""
                            };  // Filler object for the display grid.  Info can be used for CRUD operation.

                        dto.Stages.Add(badge);
                    }

                    dashboard.Add(dto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dashboard;
        }
    }
}
