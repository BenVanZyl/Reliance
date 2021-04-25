using System;

namespace Reliance.Web.Client.Dto.Organisations
{
    public class OrganisationKeyDto : BaseDto
    {
        public string OrganisationId { get; set; }
        public string PrivateKey { get; set; }
        public string Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
