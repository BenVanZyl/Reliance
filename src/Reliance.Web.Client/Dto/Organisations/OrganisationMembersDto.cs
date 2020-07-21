using System;
using System.Collections.Generic;
using System.Text;

namespace Reliance.Web.Client.Dto.Organisations
{
    public class OrganisationMembersDto
    {
        public List<OrganisationMemberDto> Items { get; set; }
        public int Count => Items != null ? Items.Count : 0;

    }
}
