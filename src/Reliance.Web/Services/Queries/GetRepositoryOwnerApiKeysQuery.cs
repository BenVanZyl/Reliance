using Reliance.Web.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Queries
{
    public class GetRepositoryOwnerApiKeysQuery : IMappableSingleItemQuery<RepositoryOwnerApiKey>
    {
        private readonly int _ownerId;

        public GetRepositoryOwnerApiKeysQuery(int ownerId)
        {
            _ownerId = ownerId;
        }

        public IQueryable<RepositoryOwnerApiKey> Execute(IQueryableProvider queryableProvider)
        {
            //get all
            var baseQuery = queryableProvider.Query<RepositoryOwnerApiKey>()
                .Where(w => w.OwnerId == _ownerId);

            return baseQuery.AsQueryable();
        }
    }
}
