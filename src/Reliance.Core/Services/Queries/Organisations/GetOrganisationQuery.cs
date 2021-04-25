
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Core.Services.Infrastructure;
using Reliance.Web.Client;
using SnowStorm.QueryExecutors;
using System.Linq;

namespace Reliance.Core.Services.Organisations.Queries
{
    public class GetOrganisationQuery : IQueryResultSingle<Organisation>
    {
        private readonly long? _id;
        private readonly string _name;
        private readonly string _masterEmail;

        public GetOrganisationQuery(long id)
        {
            _id = id;
        }
        public GetOrganisationQuery(string id)
        {
            long.TryParse(id, out long longId);
            _id = longId;
        }

        public GetOrganisationQuery(string name, string masterEmail)
        {
            _name = name;
            _masterEmail = masterEmail;
        }

        public IQueryable<Organisation> Execute(IQueryableProvider queryableProvider)
        {
            DoValidations();

            var baseQuery = queryableProvider.Query<Organisation>();

            if (_id.HasValue)
                baseQuery = baseQuery.Where(w => w.Id == _id);

            if (!string.IsNullOrWhiteSpace(_name) & !string.IsNullOrWhiteSpace(_masterEmail))
                baseQuery = baseQuery.Where(w => w.Name == _name && w.MasterEmail == _masterEmail);

            return baseQuery.AsQueryable();
        }

        private void DoValidations()
        {
            if (_id.HasValue)
                return;

            if (string.IsNullOrWhiteSpace(_name))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            if (string.IsNullOrWhiteSpace(_masterEmail))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

        }
    }
}
