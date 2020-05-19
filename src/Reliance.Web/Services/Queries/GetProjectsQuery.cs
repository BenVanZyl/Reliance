using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetProjectsQuery : IMappableQuery<Project>
    {
        private readonly long _solutionId;

        public GetProjectsQuery(long solutionId)
        {
            _solutionId = solutionId;
        }

        public IQueryable<Project> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Project>()
                            .Where(w => w.Id == _solutionId)
                            .OrderBy(o => o.Name);
            
            return baseQuery;
        }
    }
}