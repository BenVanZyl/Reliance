using System.Collections.Generic;

namespace Reliance.Web.Client.Api
{
    public class PostRepositoryDetailsDto
    {
        public string RepositoryName { get; set; }
        public long OwnerId { get; set; }
        public List<PostSolutionDto> Solutions { get; set; }
    }
}
