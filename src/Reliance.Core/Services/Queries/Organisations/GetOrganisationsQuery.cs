using Reliance.Core.Services.Domain.Organisation;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Organisations.Queries
{
    public class GetOrganisationsQuery : IQueryResultList<Organisation>
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
