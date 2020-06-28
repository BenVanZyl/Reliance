using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Dto.Organisations
{
    public class OrganisationKeyDto
    {
        public long Id { get; set; }
        public string OrganisationId { get; set; }
        public string PrivateKey { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ModifyDateTime { get; set; }
    }
}
