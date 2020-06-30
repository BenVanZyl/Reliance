using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Data.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Queries
{
    public class GetOrganisationQuery : IMappableSingleItemQuery<Organisation>
    {
        private long? _id;
        private string _name;
        private string _masterEmail;

        public GetOrganisationQuery(long id)
        {
            _id = id;
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
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Organisation Name"));
            if (string.IsNullOrWhiteSpace(_masterEmail))
                throw new ThisAppExecption(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Master Email Address"));
            //TODO: Validate that email address is valid with RegEx compare

        }
    }
}
