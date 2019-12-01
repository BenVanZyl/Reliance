using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Api
{
    public class RepositoryOwnerDto: BaseDto
    {
        public string UserId { get; set; }
        public bool IsOrganisation { get; set; }
        public string Description { get; set; }
    }
}
