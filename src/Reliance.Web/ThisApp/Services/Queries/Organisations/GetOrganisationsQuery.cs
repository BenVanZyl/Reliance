using Reliance.Web.ThisApp.Data.Organisation;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.ThisApp.Services.Organisations.Queries
{
    public class GetOrganisationsQuery : IMappableQuery<Organisation>
    {
        private readonly string _masterEmailAddress;

        public GetOrganisationsQuery(string masterEmailAddress)
        {
            _masterEmailAddress = masterEmailAddress;
        }

        public IQueryable<Organisation> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider.Query<Organisation>()
                .Where(w => w.MasterEmail == _masterEmailAddress);
            return baseQuery.AsQueryable();
        }
    }
}
