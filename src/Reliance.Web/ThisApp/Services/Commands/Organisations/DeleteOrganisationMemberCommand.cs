using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries.Organisations;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Commands.Organisations
{
    public class DeleteOrganisationMemberCommand : IRequest<bool>
    {
        public long OrganisationId { get; set; }
        public long Id { get; set; }

        public DeleteOrganisationMemberCommand(long organisationId, long id)
        {
            OrganisationId = organisationId;
            Id = id;
        }
    }

    public class DeleteOrganisationMemberCommandHandler : IRequestHandler<DeleteOrganisationMemberCommand, bool>
    {
        private readonly IQueryExecutor _executor;
        public DeleteOrganisationMemberCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<bool> Handle(DeleteOrganisationMemberCommand request, CancellationToken cancellationToken)
        {
            //do validation
            //if (long.TryParse(request.Data.OrganisationId, out long orgId))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));
            //if (orgId == 0)
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));

            var orgMember = await _executor.Execute(new GetOrganisationMemberQuery(request.Id));
            if (orgMember == null)
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Private Key record does not exists."));

            if (orgMember.OrganisationId != request.OrganisationId)
                throw new ThisAppExecption(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

            await _executor.Delete(orgMember);

            await _executor.Save();

            return true;
        }

    }
}
