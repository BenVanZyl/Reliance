using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reliance.Web.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {

        public async Task OnGet()
        {
            //init object
            if (Input == null)
            {
                Input = new InputModel()
                {
                    Id = "0",
                    Name = "",
                    MasterEmail = User.Identity.Name
                };
            }

            //get data from db

            //set grid api addapter
            WebApiAdapterUrlKeys = $"/api/oranisations/{Input.Id}/keys/";
            WebApiAdapterUrlMembers = $"/api/oranisations/{Input.Id}/members/";
        }

        public async Task OnProfileFormPost()
        {

        }

        public string WebApiAdapterUrlKeys { get; set; } = "";
        public string WebApiAdapterUrlMembers { get; set; } = "";

        [TempData]
        public string StatusMessage { get; set; } = "";

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Organisation Id")]
            public string Id { get; set; }

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
}

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.1&tabs=visual-studio