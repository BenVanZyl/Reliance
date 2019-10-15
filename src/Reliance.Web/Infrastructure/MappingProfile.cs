using AutoMapper;
using Reliance.Web.Client.Api;
using Reliance.Web.Domain;

namespace Reliance.Web.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Repository, RepositoryDto>();

            CreateMap<Solution, SolutionDto>();

            CreateMap<Project, ProjectDto>();

            CreateMap<Package, PackageDto>()
                .ForMember(t => t.Description, m => m.Ignore());

            CreateMap<ProjectPackage, ProjectPackageDto>();

            //CreateMap<Package, SelectList>()
            //    .ForMember(t => t.)
        }

    }
}
