
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Core.Services.Infrastructure;
using Reliance.Web.Client;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Queries.Organisations
{
    public class GetOrganisationMembersQuery : IQueryResultList<Member>
    {
        private readonly long? _organisationId;
        private readonly string _email;

        public GetOrganisationMembersQuery(long organisationId)
        {
            _organisationId = organisationId;
        }

        public GetOrganisationMembersQuery(long organisationId, string email)
        {
            _organisationId = organisationId;
            _email = email;
        }

        public IQueryable<Member> Execute(IQueryableProvider queryableProvider)
        {
            //validate query data
            if (!_organisationId.HasValue && string.IsNullOrWhiteSpace(_email))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Member information"));

            var baseQuery = queryableProvider.Query<Member>()
                .Where(w => w.OrganisationId == _organisationId);

            if (!string.IsNullOrWhiteSpace(_email))
                baseQuery = baseQuery.Where(w =>  w.Email == _email);

            return baseQuery.AsQueryable();
        }
    }
}
