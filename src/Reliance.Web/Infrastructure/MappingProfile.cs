using AutoMapper;
using Reliance.Web.Client.Api;
using Reliance.Web.Domain;

namespace Reliance.Web.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<RepositoryOwner, RepositoryOwnerDto>();

            CreateMap<RepositoryOwnerApiKey, RepositoryOwnerApiKeyDto>()
                .ForMember(t => t.CreatedOn, m => m.MapFrom(s => s.CreateDateTime));

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
