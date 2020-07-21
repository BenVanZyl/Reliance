
namespace Reliance.Web.Client.Dto.Organisations
{
    public class OrganisationMemberDto : BaseDto
    {
        public string OrganisationId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public long OrgId
        {
            get
            {
                long.TryParse(OrganisationId, out long orgId);
                return orgId;
            }
        }
    }
}
