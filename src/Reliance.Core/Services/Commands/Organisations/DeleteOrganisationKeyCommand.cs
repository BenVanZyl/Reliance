﻿using MediatR;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Queries.Organisations;
using Reliance.Web.Client;
using SnowStorm.QueryExecutors;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Commands.Organisations
{
    public class DeleteOrganisationKeyCommand : IRequest<bool>
    {
        public long OrganisationId { get; set; }
        public long Id { get; set; }

        public DeleteOrganisationKeyCommand(long organisationId, long id)
        {
            OrganisationId = organisationId;
            Id = id;
        }
    }

    public class DeleteOrganisationKeyCommandHandler : IRequestHandler<DeleteOrganisationKeyCommand, bool>
    {
        private readonly IQueryExecutor _executor;
        public DeleteOrganisationKeyCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<bool> Handle(DeleteOrganisationKeyCommand request, CancellationToken cancellationToken)
        {
            //do validation
            //if (long.TryParse(request.Data.OrganisationId, out long orgId))
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));
            //if (orgId == 0)
            //    throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));

            var orgKey = await _executor.Execute(new GetOrganisationKeyQuery(request.Id));
            if (orgKey == null)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Private Key record does not exists."));

            if (orgKey.OrganisationId != request.OrganisationId)
                throw new ThisAppException(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

            await _executor.Delete(orgKey);

            await _executor.Save();

            return true;
        }

    }
}
