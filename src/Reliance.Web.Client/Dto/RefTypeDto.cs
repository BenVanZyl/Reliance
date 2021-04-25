using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Dto
{
    public class RefTypeDto
    {
        public long Id { get; set; }
        public long OrganisationId { get; set; }
        public string Name { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public string LastModifiedOn => ModifyDateTime.ToString("yyyy-MM-dd HH:mm");
    }
}
