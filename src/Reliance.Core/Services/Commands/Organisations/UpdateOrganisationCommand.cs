using AutoMapper;
using MediatR;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Organisations.Queries;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Organisations.Commands
{
    public class UpdateOrganisationCommand : IRequest<OrganisationDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MasterEmail { get; set; }

        public UpdateOrganisationCommand(string id, string name, string masterEmail)
        {
            if (!long.TryParse(id, out long idValue))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation"));

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

    public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand, OrganisationDto>
    {
        private readonly IQueryExecutor _executor;
        private readonly IMapper _mapper;
        // TODO: Implement auditing
        public UpdateOrganisationCommandHandler(IQueryExecutor executor, IMapper mapper)
        {
            _executor = executor;
            _mapper = mapper;
            
        }
        public async Task<OrganisationDto> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            //do validation
            if (request.Id == 0)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation"));
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            if (string.IsNullOrWhiteSpace(request.MasterEmail))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

            //get existing and validate master email address to confirm that the change is allowed. create if nothing found.
            var org = await _executor.Execute(new GetOrganisationQuery(request.Id));
            if (org == null)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation"));

            org.SetName(request.Name);
            org.SetMasterEmail(request.MasterEmail);

            await _executor.Save();

            return _mapper.Map<OrganisationDto>(org);
        }
    }
}

