using MediatR;
using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Data.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Organisations.Queries;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Organisations.Commands
{
    public class UpdateOrganisationCommand : IRequest<Organisation>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MasterEmail { get; set; }

        public UpdateOrganisationCommand(string id, string name, string masterEmail)
        {
            if (!long.TryParse(id, out long idValue))
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation"));

            Id = idValue;
            Name = name;
            MasterEmail = masterEmail;
        }

        public UpdateOrganisationCommand(long id, string name, string masterEmail)
        {
            Id = id;
            Name = name;
            MasterEmail = masterEmail;
        }
    }

    public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand, Organisation>
    {
        private readonly IQueryExecutor _executor;
        //private readonly IUserRepository _userRepository;  // TODO: Implement auditing
        public UpdateOrganisationCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
            //_userRepository = userRepository;
        }
        public async Task<Organisation> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            //do validation
            if (request.Id == 0)
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation"));
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            if (string.IsNullOrWhiteSpace(request.MasterEmail))
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

            //get existing and validate master email address to confirm that the change is allowed. create if nothing found.
            var org = await _executor.Execute(new GetOrganisationQuery(request.Id));
            if (org == null)
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation"));

            org.SetName(request.Name);
            org.SetMasterEmail(request.MasterEmail);

            await _executor.Save();

            return org;
        }
    }
}

