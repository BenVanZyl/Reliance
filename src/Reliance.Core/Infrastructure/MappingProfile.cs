using AutoMapper;
using Reliance.Core.Services.Domain.Organisation;
using Reliance.Web.Client.Dto.Organisations;

namespace Reliance.Core.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organisation, OrganisationDto>();

            CreateMap<OrganisationKey, OrganisationKeyDto>()
                .ForMember(d => d.OrganisationId, m => m.MapFrom(s => s.OrganisationId.ToString()))
                .ForMember(d => d.ExpiryDate, m => m.MapFrom(s => s.ExpiryDate));

            CreateMap<Member, OrganisationMemberDto>()
                .ForMember(d => d.OrganisationId, m => m.MapFrom(s => s.OrganisationId.ToString()));
        }
    }
}
