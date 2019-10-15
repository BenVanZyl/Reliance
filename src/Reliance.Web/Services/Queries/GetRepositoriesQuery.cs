using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoriesQuery : IMappableQuery<Repository>
    {
        public GetRepositoriesQuery() { }

        public IQueryable<Repository> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Repository>()
                .OrderBy(o => o.Name);

            return baseQuery.AsQueryable();
        }
    }
}