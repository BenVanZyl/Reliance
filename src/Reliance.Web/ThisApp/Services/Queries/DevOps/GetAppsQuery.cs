using Reliance.Web.ThisApp.Domain.DevOps;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries.DevOps
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
