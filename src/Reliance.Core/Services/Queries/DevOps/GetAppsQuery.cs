using Reliance.Core.Services.Domain.DevOps;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Queries.DevOps
{
    public class GetAppsQuery : IQueryResultList<App>
    {
        private readonly long _orgId;

        public GetAppsQuery(long organisationId)
        {
            _orgId = organisationId;
        }

        public IQueryable<App> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider
                .Query<App>()
                .Where(w => w.OrganisationId == _orgId)
                .OrderBy(o => o.Name);

            return baseQuery.AsQueryable();
        }
    }
}
