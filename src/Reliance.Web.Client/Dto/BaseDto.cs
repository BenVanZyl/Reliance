using System;

namespace Reliance.Web.Client.Dto
{
    public class BaseDto
    {
        public long Id { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string ModifyDateString => ModifiedOn.ToString("yyyy-MM-dd HH:mm:ss");
        public string ModifiedOnString => ModifiedOn.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
