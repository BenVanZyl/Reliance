using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoriesQuery : IMappableQuery<Repository>
    {
        private long? _packageId;
        private long _ownerId;

        public GetRepositoriesQuery(long ownerId)
        {
            _ownerId = ownerId;
            _packageId = null;
        }
        public GetRepositoriesQuery(long ownerId, long packageId) 
        {
            _ownerId = ownerId;
            _packageId = packageId;
        }

        public IQueryable<Repository> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Repository>()
                .Where(w => w.OwnerId == _ownerId);

            //if (_packageId.HasValue)
            //    baseQuery = baseQuery.Where(w => w. == _packageId);

            baseQuery = baseQuery.OrderBy(o => o.Name);
            return baseQuery.AsQueryable();
        }
    }
}