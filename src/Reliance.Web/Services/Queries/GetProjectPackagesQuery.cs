using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetProjectPackagesQuery : IMappableQuery<ProjectPackage>
    {

        private readonly int? _projectId;
        private readonly int? _packageId;

        /// <summary>
        /// Supply ProjectId to list all packages for a project or PackageId to list all projects for a package
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="packageId"></param>
        public GetProjectPackagesQuery(int? projectId, int? packageId)
        {
            _projectId = projectId;
            _packageId = packageId;
        }

        public IQueryable<ProjectPackage> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<ProjectPackage>();
            
            //List all packages for a project
            if (_projectId.HasValue)
                baseQuery = baseQuery.Where(w => w.ProjectId == _projectId)
                                    .OrderBy(o => o.Package.Name);

            //List all projects for a package
            if (_packageId.HasValue)
                baseQuery = baseQuery.Where(w => w.PackageId== _packageId)
                                    .OrderBy(o => o.Project.Name);
            
            return baseQuery.AsQueryable();
        }
    }
}
