using System.Collections.Generic;

namespace Reliance.Web.Client.Dto.Organisations
{
    /// <summary>
    /// SyncFusion Grids
    /// </summary>
    public class OrganisationKeysDto
    {
        public List<OrganisationKeyDto> Items { get; set; }
        public int Count => Items != null ? Items.Count : 0;

    }
}
