using MediatR;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Commands.Organisations
{
    public class CreateOrganisationKeyCommand : IRequest<OrganisationKey>
    {
        public OrganisationKeyDto Data { get; set; }

        public CreateOrganisationKeyCommand(OrganisationKeyDto data)
        {
            Data = data;
        }
    }

    public class CreateOrganisationKeyCommandHandler : IRequestHandler<CreateOrganisationKeyCommand, OrganisationKey>
    {
        private readonly IQueryExecutor _executor;
        public CreateOrganisationKeyCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<OrganisationKey> Handle(CreateOrganisationKeyCommand request, CancellationToken cancellationToken)
        {
            //do validation
            //if (string.IsNullOrWhiteSpace(request.Data.OrganisationId))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            //if (string.IsNullOrWhiteSpace(request.MasterEmail))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

            var orgKey = await OrganisationKey.Create(_executor, request.Data);

            return orgKey;
        }
    }
}
