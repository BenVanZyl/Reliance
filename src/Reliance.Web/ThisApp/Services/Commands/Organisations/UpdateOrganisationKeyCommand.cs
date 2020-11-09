using MediatR;
using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Domain.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries.Organisations;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Commands.Organisations
{
    public class UpdateOrganisationKeyCommand : IRequest<OrganisationKey>
    {
        public OrganisationKeyDto Data { get; set; }

        public UpdateOrganisationKeyCommand(OrganisationKeyDto data)
        {
            Data = data;
        }
    }

    public class UpdateOrganisationKeyCommandHandler : IRequestHandler<UpdateOrganisationKeyCommand, OrganisationKey>
    {
        private readonly IQueryExecutor _executor;
        public UpdateOrganisationKeyCommandHandler(IQueryExecutor executor)
        {
            _executor = executor;
        }
        public async Task<OrganisationKey> Handle(UpdateOrganisationKeyCommand request, CancellationToken cancellationToken)
        {
            //do validation
            if (!long.TryParse(request.Data.OrganisationId, out long orgId))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));
            if (orgId == 0)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Id"));

            var orgKey = await _executor.Execute(new GetOrganisationKeyQuery(request.Data.Id));
            if (orgKey == null)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Private Key record does not exists."));

            if (orgKey.OrganisationId != orgId)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation Id"));

            if (orgKey.PrivateKey != request.Data.PrivateKey)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Private Key"));

            if (!request.Data.ExpiryDate.HasValue)
                request.Data.ExpiryDate = DateTime.Now.AddYears(3);

            orgKey.SetDescription(request.Data.Description);
            orgKey.SetExpiryDate(request.Data.ExpiryDate.Value);

            await _executor.Save();

            return orgKey;
        }

    }
}
