using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoriesQuery : IMappableQuery<Repository>
    {
        private int? _packageId;
        private int _ownerId;

        public GetRepositoriesQuery(int ownerId)
        {
            _ownerId = ownerId;
            _packageId = null;
        }
        public GetRepositoriesQuery(int ownerId, int packageId) 
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