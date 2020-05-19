using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoryOwnerQuery : IMappableSingleItemQuery<RepositoryOwner>
    {
        private readonly long? _id;
        private readonly string _userId;

        public GetRepositoryOwnerQuery(long id)
        {
            _id = id;
        }

        public GetRepositoryOwnerQuery(string userId)
        {
            _id = null;
            _userId = userId;
        }

        public IQueryable<RepositoryOwner> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<RepositoryOwner>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrEmpty(_userId))
                baseQuery = baseQuery.Where(w => w.UserId == _userId);

            return baseQuery.AsQueryable();
        }
    }
}
