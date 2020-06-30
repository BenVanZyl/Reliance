using Reliance.Web.ThisApp.Data.Organisation;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries
{
    public class GetOrganisationsQuery : IMappableQuery<Organisation>
    {
        private string _masterEmailAddress;

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
