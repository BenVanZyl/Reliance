using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoryQuery : IMappableSingleItemQuery<Repository>
    {
        //TIP: Include(fk => fk.FkClass)
        //TIP: Decompile() for use with Computed fields

        private readonly int? _id;
        private readonly string _name;
        private readonly int? _ownerId;

        public GetRepositoryQuery(int id)
        {
            _id = id;
            _name = "";
            _ownerId = null;
        }

        public GetRepositoryQuery(string name, int ownerId)
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
