using Reliance.Web.ThisApp.Domain.DevOps;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries.DevOps
{
    public class GetStagesQuery : IQueryResultList<Stage>
    {
        private readonly long _orgId;

        public GetStagesQuery(long organisationId)
        {
            _orgId = organisationId;
        }

        public IQueryable<Stage> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider
                .Query<Stage>()
                .Where(w => w.OrganisationId == _orgId)
                .OrderBy(o => o.OrderBy);

            return baseQuery.AsQueryable();
        }
    }
}
