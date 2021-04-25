using Reliance.Core.Services.Domain.DevOps;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Queries.DevOps
{
    public class GetStagesQuery : IQueryResultList<Stage>
    {
        private readonly long _orgId;

        public GetStagesQuery(long orgId)
        {
            _orgId = orgId;
        }

        public IQueryable<Stage> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider
                .Query<Stage>()
                .Where(w => w.OrganisationId == _orgId)
                .OrderBy(o => o.OrderBy); //defined sort order

            return baseQuery.AsQueryable();
        }
    }
}
