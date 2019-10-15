using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetPackagesQuery : IMappableQuery<Package>
    {
        public IQueryable<Package> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Package>()
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
