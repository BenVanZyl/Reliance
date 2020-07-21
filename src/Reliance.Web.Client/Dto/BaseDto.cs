using System;

namespace Reliance.Web.Client.Dto
{
    public class BaseDto
    {
        public long Id { get; set; }
        public DateTime ModifyDateTime { get; set; }

        public string ModifyDateString => ModifyDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        public string ModifyDateTimeString => ModifyDateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
