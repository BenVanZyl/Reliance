using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Dto.Dashboard
{
    public class DashboardDto
    {
        public long AppId { get; set; }
        public string AppName { get; set; }
        public List<BadgeDto> Stages { get; set; }
    }
}
