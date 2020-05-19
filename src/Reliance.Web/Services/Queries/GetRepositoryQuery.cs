using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoryQuery : IMappableSingleItemQuery<Repository>
    {
        //TIP: Include(fk => fk.FkClass)
        //TIP: Decompile() for use with Computed fields

        private readonly long? _id;
        private readonly string _name;
        private readonly long? _ownerId;

        public GetRepositoryQuery(long id)
        {
            _id = id;
            _name = "";
            _ownerId = null;
        }

        public GetRepositoryQuery(string name, long ownerId)
        {
            _id = null;
            _name = name;
            _ownerId = ownerId;
        }

        public IQueryable<Repository> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Repository>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrEmpty(_name))
                baseQuery = baseQuery.Where(w => w.Name == _name);

            if (_ownerId.HasValue)
                baseQuery = baseQuery.Where(w => w.OwnerId == _ownerId.Value);

            return baseQuery.AsQueryable();
        }
    }
}
