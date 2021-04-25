using MediatR;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Queries.DevOps;
using Reliance.Web.Client.Dto.Dashboard;
using SnowStorm.QueryExecutors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Commands.DevOps
{
    public class SaveBadgeCommand : IRequest<BadgeDto>
    {
        public BadgeDto Badge { get; set; }

        public SaveBadgeCommand(BadgeDto badge)
        {
            Badge = badge;
        }
    }

    public class SaveBadgeCommandHandler : IRequestHandler<SaveBadgeCommand, BadgeDto>
    {
        private readonly IQueryExecutor _executor;
        public SaveBadgeCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<BadgeDto> Handle(SaveBadgeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Badge.AppId == 0)
                    throw new ThisAppException(StatusCodes.Status400BadRequest, "App info not provided!");

                if (request.Badge.StageId == 0)
                    throw new ThisAppException(StatusCodes.Status400BadRequest, "Stage info not provided!");

                if (string.IsNullOrWhiteSpace(request.Badge.BadgeUrl))
                    throw new ThisAppException(StatusCodes.Status400BadRequest, "Badge Url not provided!");

                // get app
                var app = await _executor.Execute(new GetAppQuery(request.Badge.AppId));
                if (app == null)
                    throw new ThisAppException(StatusCodes.Status404NotFound, "No App found");

                // get stage
                var stages = await _executor.Execute(new GetStageQuery(request.Badge.StageId));
                if (stages == null)
                    throw new ThisAppException(StatusCodes.Status404NotFound, "No Stage found");

                // get badge
                var badge = await _executor.Execute(new GetBadgeQuery(request.Badge.Id));
                if (badge == null)
                {
                    badge = await Domain.DevOps.Badge.Create
                            (
                                _executor,
                                request.Badge.AppId,
                                request.Badge.StageId,
                                request.Badge.BadgeUrl
                            );

                    request.Badge.Id = badge.Id;
                }
                else
                {
                    badge.SetBadgeUrl(request.Badge.BadgeUrl);
                }

                await _executor.Save();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return request.Badge;
        }
    }
}
