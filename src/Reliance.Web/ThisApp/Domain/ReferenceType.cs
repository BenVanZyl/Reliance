using SnowStorm.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Domain
{
    public class ReferenceType : DomainEntityWithIdWithAudit
    {
        protected ReferenceType() { }

        #region Properties

        public long OrganisationId { get; set; }

        public string Name { get; private set; }
        
        #endregion //properties

        #region Methods
        public ReferenceType(long organisationId, string name)
        {
            SetOrganisationId(organisationId);
            SetName(name);
        }
        public void SetOrganisationId(long value)
        {
            if (OrganisationId != value)
                OrganisationId = value;
        }

        public void SetName(string value)
        {
            if (Name != value)
                Name = value;
        }

        #endregion //methods
    }
}
