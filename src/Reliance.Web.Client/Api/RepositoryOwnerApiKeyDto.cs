using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Api
{
    public class RepositoryOwnerApiKeyDto
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string ApiKey { get; set; }
        public DateTime ExpireOn { get; set; }
        public bool IsRevoked { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
