
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Core.Services.Infrastructure;
using Reliance.Web.Client;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Queries.Organisations
{
    public class GetOrganisationMemberQuery : IQueryResultSingle<Member>
    {
        private readonly long? _id;

        private readonly long? _orgId;
        private readonly string _email;

        public GetOrganisationMemberQuery(long id)
        {
            _id = id;
        }

        public GetOrganisationMemberQuery(long orgId, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ThisAppException(StatusCodes.Status412PreconditionFailed, Messages.Err417MissingObjectData("Email Address"));

            _orgId = orgId;
            _email = email;
        }

        public IQueryable<Member> Execute(IQueryableProvider queryableProvider)
        {
            if (!_id.HasValue && !_orgId.HasValue)
                throw new ThisAppException(StatusCodes.Status412PreconditionFailed, Messages.Err417MissingObjectData(""));

            var baseQuery = queryableProvider.Query<Member>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id.Value);

            if (_orgId.HasValue)
                baseQuery = baseQuery.Where(w => w.OrganisationId == _orgId.Value && w.Email == _email);

            return baseQuery.AsQueryable();
        }
    }
}
