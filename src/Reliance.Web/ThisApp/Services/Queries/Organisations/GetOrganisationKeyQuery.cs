using Microsoft.EntityFrameworkCore;
using Reliance.Web.ThisApp.Domain.Organisation;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries.Organisations
{
    public class GetOrganisationKeyQuery : IQueryResultSingle<OrganisationKey>
    {
        private readonly long _id;

        private bool _includeOrganisation = false;

        public GetOrganisationKeyQuery(long id)
        {
            _id = id;
        }

        public GetOrganisationKeyQuery IncludeOrganisation()
        {
            _includeOrganisation = true;
            return this;
        }

        public IQueryable<OrganisationKey> Execute(IQueryableProvider queryableProvider)
        {
            var baseQuery = queryableProvider.Query<OrganisationKey>()
                .Where(w => w.Id == _id);

            if (_includeOrganisation)
                baseQuery = baseQuery.Include(i => i.Organisation);

            return baseQuery.AsQueryable();
        }
    }
}
