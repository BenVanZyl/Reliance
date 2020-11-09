using AutoMapper;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Domain.Organisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Infrastructure
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
