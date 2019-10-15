using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetProjectsQuery : IMappableQuery<Project>
    {
        private readonly int _solutionId;

        public GetProjectsQuery(int solutionId)
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