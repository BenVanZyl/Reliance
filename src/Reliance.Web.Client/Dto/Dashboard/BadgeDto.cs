using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Dto.Dashboard
{
    public class BadgeDto
    {
        public long Id { get; set; }
        public long AppId { get; set; }
        //public string AppName { get; set; }
        public long StageId { get; set; }
        //public string StageName { get; set; }
        public string BadgeUrl { get; set; }
    }
}
