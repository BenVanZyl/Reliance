using AutoMapper;
using MediatR;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Core.Services.Infrastructure;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Organisations.Commands
{
    public class CreateOrganisationCommand : IRequest<OrganisationDto>
    {
        public string Name { get; set; }
        public string MasterEmail { get; set; }

        public CreateOrganisationCommand(string name, string masterEmail)
        {
            Name = name;
            MasterEmail = masterEmail;
        }
    }

    public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, OrganisationDto>
    {
        private readonly IQueryExecutor _executor;
        private readonly IMapper _mapper;

        public CreateOrganisationCommandHandler(IQueryExecutor executor, IMapper mapper)
        {
            _executor = executor;
            _mapper = mapper;
        }

        public async Task<OrganisationDto> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
        {
            //do validation
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            if (string.IsNullOrWhiteSpace(request.MasterEmail))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

            var newOrg = await Organisation.Create(_executor, request.Name, request.MasterEmail);

            return _mapper.Map<OrganisationDto>(newOrg);
        }
    }
}

