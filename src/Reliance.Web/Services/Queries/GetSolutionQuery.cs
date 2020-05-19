using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetSolutionQuery : IMappableSingleItemQuery<Solution>
    {
        private readonly long? _id;
        private string _name;
        private long? _repositoryId;

        public GetSolutionQuery(long id)
        {
            _id = id;
            _name = "";
            _repositoryId = null;
        }

        public GetSolutionQuery(string name, long repositoryId)
        {
            _id = null;
            _name = name;
            _repositoryId = repositoryId;
        }

        public IQueryable<Solution> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Solution>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrEmpty(_name))
                baseQuery = baseQuery.Where(w => w.Name == _name && w.RepositoryId == _repositoryId);

            return baseQuery.AsQueryable();
        }
    }
}
