using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetSolutionsQuery : IMappableQuery<Solution>
    {
        private long? _repositoryId = null;

        public GetSolutionsQuery(long repositoryId)
        {
            _repositoryId = repositoryId;
        }

        public IQueryable<Solution> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Solution>()
                            .Where(w => w.RepositoryId == _repositoryId)
                            .OrderBy(o => o.Name);

            return baseQuery.AsQueryable();
        }
    }
}
