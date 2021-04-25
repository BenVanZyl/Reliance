using System.ComponentModel.DataAnnotations;

namespace Reliance.Web.Client.Dto.Organisations
{
    public class OrganisationDto
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Organisation Id")]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Organisation Name")]
        public string Name { get; set; }

        [EmailAddress]
        [Display(Name = "Master Email")]
        [DataType(DataType.Text)]
        public string MasterEmail { get; set; }
    }
}
