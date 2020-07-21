using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Data.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.ThisApp.Services.Queries.Organisations
{
    public class GetOrganisationMemberQuery : IMappableSingleItemQuery<Member>
    {
        private readonly long _id;

        public GetOrganisationMemberQuery(long id)
        {
            _id = id;
        }

        public IQueryable<Member> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider.Query<Member>()
                .Where(w => w.Id == _id);

            return baseQuery.AsQueryable();
        }
    }
}
