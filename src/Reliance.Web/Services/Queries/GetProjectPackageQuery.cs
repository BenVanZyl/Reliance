using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetProjectPackageQuery : IMappableSingleItemQuery<ProjectPackage>
    {
        private readonly int _projectId;
        private readonly int _packageId;

        public GetProjectPackageQuery(int projectId, int packageId)
        {
            _projectId = projectId;
            _packageId = packageId;
        }

        public IQueryable<ProjectPackage> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<ProjectPackage>()
                 .Where(w => w.ProjectId == _projectId 
                           && w.PackageId == _packageId)
                 .AsQueryable();
            return baseQuery;
        }
    }
}
