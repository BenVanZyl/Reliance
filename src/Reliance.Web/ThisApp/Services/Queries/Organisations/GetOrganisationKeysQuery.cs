using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Domain.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries.Organisations
{
    public class GetOrganisationKeysQuery : IQueryResultList<OrganisationKey>
    {
        private readonly long? _organisationId;
        private readonly OrganisationKeyDto _data;

        public GetOrganisationKeysQuery(long organisationId)
        {
            _organisationId = organisationId;
        }

        public GetOrganisationKeysQuery(OrganisationKeyDto data)
        {
            _data = data;
            long.TryParse(data.OrganisationId, out long orgId);
            _organisationId = orgId;
        }

        public IQueryable<OrganisationKey> Execute(IQueryableProvider queryableProvider)
        {
            //validate query data
            if (!_organisationId.HasValue && _data == null)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Private Key information"));

            var baseQuery = queryableProvider.Query<OrganisationKey>()
                .Where(w => w.OrganisationId == _organisationId);

            if (_data != null)
            {
                baseQuery = baseQuery.Where
                    (
                        w =>
                        w.Description == _data.Description &&
                        w.ExpiryDate == _data.ExpiryDate &&
                        w.PrivateKey == _data.PrivateKey
                    );
            }

            return baseQuery.AsQueryable();
        }
    }
}
