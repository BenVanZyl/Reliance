using System.Collections.Generic;

namespace Reliance.Web.Client.Api
{
    public class PostProjectDto
    {
        public string ProjectName { get; set; }

        public List<PackageDto> Packages { get; set; }
    }
}
