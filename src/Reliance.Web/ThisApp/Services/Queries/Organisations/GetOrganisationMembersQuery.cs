using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Domain.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Linq;

namespace Reliance.Web.ThisApp.Services.Queries.Organisations
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
