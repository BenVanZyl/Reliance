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

        public GetRepositoryQuery(int id)
        {
            _id = id;
            _name = "";
        }

        public GetRepositoryQuery(string name)
        {
            _name = name;

        }
        public IQueryable<Repository> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Repository>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrEmpty(_name))
                baseQuery = baseQuery.Where(w => w.Name == _name);

            return baseQuery.AsQueryable();
        }
    }
}
