using MediatR;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Organisations.Queries;
using Reliance.Web.Client;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Commands.Organisations
{
    public class DeleteOrganisationCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public DeleteOrganisationCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteOrganisationCommandHandler : IRequestHandler<DeleteOrganisationCommand, bool>
    {
        private readonly IQueryExecutor _executor;
        public DeleteOrganisationCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<bool> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
        {
            //TODO: do validation
            //if (long.TryParse(request.Data.OrganisationId, out long orgId))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));
            //if (orgId == 0)
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));

            var org = await _executor.Execute(new GetOrganisationQuery(request.Id));
            if (org == null)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Private Key record does not exists."));

            if (org.Id != request.Id)
                throw new ThisAppException(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

            await _executor.Delete(org);

            await _executor.Save();

            return true;
        }

    }
}
