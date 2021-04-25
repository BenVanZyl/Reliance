using Microsoft.EntityFrameworkCore;
using Reliance.Core.Services.Domain.Organisation;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Queries.Organisations
{
    public class GetOrganisationKeyQuery : IQueryResultSingle<OrganisationKey>
    {
        private readonly long _id;

        private bool _includeOrganisation = false;

        public GetOrganisationKeyQuery(long id)
        {
            _id = id;
        }

        public GetOrganisationKeyQuery IncludeOrganisation()
        {
            _includeOrganisation = true;
            return this;
        }

        public IQueryable<OrganisationKey> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider.Query<OrganisationKey>()
                .Where(w => w.Id == _id);

            if (_includeOrganisation)
                baseQuery = baseQuery.Include(i => i.Organisation);

            return baseQuery.AsQueryable();
        }
    }
}
