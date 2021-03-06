﻿using MediatR;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Commands.Organisations
{
    public class CreateOrganisationMemberCommand : IRequest<Member>
    {
        public OrganisationMemberDto Data { get; set; }

        public CreateOrganisationMemberCommand(OrganisationMemberDto data)
        {
            Data = data;
        }
    }

    public class CreateOrganisationMemberCommandHandler : IRequestHandler<CreateOrganisationMemberCommand, Member>
    {
        private readonly IQueryExecutor _executor;
        public CreateOrganisationMemberCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<Member> Handle(CreateOrganisationMemberCommand request, CancellationToken cancellationToken)
        {
            //do validation
            //if (string.IsNullOrWhiteSpace(request.Data.OrganisationId))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            //if (string.IsNullOrWhiteSpace(request.MasterEmail))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

            var orgMember = await Member.Create(_executor, request.Data);

            return orgMember;
        }
    }
}
