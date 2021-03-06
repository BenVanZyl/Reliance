﻿using MediatR;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Queries.Organisations;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Commands.Organisations
{
    public class UpdateOrganisationMemberCommand : IRequest<Member>
    {
        public OrganisationMemberDto Data { get; set; }

        public UpdateOrganisationMemberCommand(OrganisationMemberDto data)
        {
            Data = data;
        }
    }

    public class UpdateOrganisationMemberCommandHandler : IRequestHandler<UpdateOrganisationMemberCommand, Member>
    {
        private readonly IQueryExecutor _executor;
        public UpdateOrganisationMemberCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<Member> Handle(UpdateOrganisationMemberCommand request, CancellationToken cancellationToken)
        {
            //do validation
            if (!long.TryParse(request.Data.OrganisationId, out long orgId))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));
            if (orgId == 0)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));

            var orgMember = await _executor.Execute(new GetOrganisationMemberQuery(request.Data.Id));
            if (orgMember == null)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Private Key record does not exists."));

            if (orgMember.OrganisationId != orgId)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation Id"));

            orgMember.SetName(request.Data.Name);
            orgMember.SetEmail(request.Data.Email);
            orgMember.SetIsActive(request.Data.IsActive);

            await _executor.Save();

            return orgMember;
        }

    }
}
