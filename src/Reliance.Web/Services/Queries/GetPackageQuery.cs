using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetPackageQuery : IMappableSingleItemQuery<Package>
    {
        private readonly Client.Api.PackageDto _data;
        private readonly int? _packageId;

        public GetPackageQuery(int packageId)
        {
            _packageId = packageId;
            _data = null;
        }

        public GetPackageQuery(Client.Api.PackageDto data)
        {
            _packageId = null;
            _data = data;

            if (_data == null)
                throw new Exception("GetPackageQuery : No identifying data supplied!");
        }

        public IQueryable<Package> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Package>();

            if (_packageId.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _packageId);

            if (_data != null)
                baseQuery = baseQuery.Where(w => w.Name == _data.Name
                                 && w.VersionMaster == _data.VersionMaster
                                 && w.VersionMinor == _data.VersionMinor
                                 && w.VersionPatch == _data.VersionPatch
                                 && w.TargetFrameWork == _data.TargetFrameWork);
                 
            return baseQuery.AsQueryable(); 
        }
    }
}
