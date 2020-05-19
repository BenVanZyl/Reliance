using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.Services.Queries
{
    public class GetProjectQuery : IMappableSingleItemQuery<Project>
    {

        private readonly long? _id;
        private string _name;
        private long? _solutionId;

        public GetProjectQuery(long id)
        {
            _id = id;
            _name = "";
            _solutionId = null;
        }

        public GetProjectQuery(string name, long solutionId)
        {
            _id = null;
            _name = name;
            _solutionId = solutionId;
        }

        public IQueryable<Project> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<Project>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrEmpty(_name))
                baseQuery = baseQuery.Where(w => w.Name == _name && w.SolutionId == _solutionId);

            return baseQuery.AsQueryable();
        }
    }
}

