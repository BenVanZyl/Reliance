using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetPackagesQuery : IMappableQuery<Package>
    {
        private string packageName;

        public GetPackagesQuery() { }

        public GetPackagesQuery(string packageName)
        {
            this.packageName = packageName;
        }

        public IQueryable<Package> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Package>();

            if (!string.IsNullOrWhiteSpace(packageName))
                baseQuery = baseQuery.Where(w => w.Name == packageName);

            baseQuery = baseQuery
                .OrderBy(o => o.Name)
                .ThenBy(o => o.VersionMaster)
                .ThenBy(o => o.VersionMinor)
                .ThenBy(o => o.VersionPatch)
                .ThenBy(o => o.TargetFrameWork)
                .AsQueryable();

            return baseQuery;
        }
    }
}
