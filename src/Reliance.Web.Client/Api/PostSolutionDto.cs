using System.Collections.Generic;

namespace Reliance.Web.Client.Api
{
    public class PostSolutionDto
    {
        public string SolutionName { get; set; }
        public List<PostProjectDto> Projects { get; set; }
    }
}
