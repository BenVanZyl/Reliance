using MediatR;
using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Data.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Commands
{
    public class CreateOrganisationCommand : IRequest<Organisation>
    {
        public string Name { get; set; }
        public string MasterEmail { get; set; }

        public CreateOrganisationCommand(string name, string masterEmail)
        {
            Name = name;
            MasterEmail = masterEmail;
        }
    }

    public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, Organisation>
    {
        private readonly IQueryExecutor _executor;
        public CreateOrganisationCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<Organisation> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            //do validation
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            if (string.IsNullOrWhiteSpace(request.MasterEmail))
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

            var org = await Organisation.Create(_executor, request.Name, request.MasterEmail);

            return org;
        }
    }
}

