using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Data.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Organisations.Commands;
using Reliance.Web.ThisApp.Services.Organisations.Queries;
using SnowStorm.Infrastructure.QueryExecutors;

namespace Reliance.Web.Pages
{
    [Authorize]
    public class ProfileModel : BasePageModel
    {
        public ProfileModel(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        public async Task OnGet()
        {
            await GetData();
        }

        private async Task GetData()
        {
            //init object
            if (OrgData == null || OrgData.MasterEmail != User.Identity.Name)
            {
                OrgData = new OrgDataModel()
                {
                    Id = "0",
                    Name = User.Identity.Name.Replace("@", " at "),
                    MasterEmail = User.Identity.Name
                };

                //get data from db
                var org = await Executor.Execute(new GetOrganisationsQuery(User.Identity.Name));
                if (org != null && org.Count > 0)
                {   // TODO: List in dropdown for user to select
                    var o = org.First();
                    OrgData.Id = o.Id.ToString();
                    OrgData.Name = o.Name;
                    OrgData.MasterEmail = o.MasterEmail;
                }
                else
                {
                    var orgNew = await Mediator.Send(new CreateOrganisationCommand(OrgData.Name, OrgData.MasterEmail));
                    if (orgNew != null)
                        OrgData.Id = orgNew.Id.ToString();
                }
            }

            //set grid api addapter
            WebApiAdapterUrlKeys = $"/api/organisations/{OrgData.Id}/keys";
            WebApiAdapterUrlMembers = $"/api/organisations/{OrgData.Id}/members";
        }

        public async Task OnPostProfileForm()
        {
            try
            {
                var org = await Mediator.Send(new UpdateOrganisationCommand(OrgData.Id, OrgData.Name, OrgData.MasterEmail));

                OrgData.Id = org.Id.ToString();
                OrgData.Name = org.Name;
                OrgData.MasterEmail = org.MasterEmail;
            }
            catch (ThisAppExecption ex)
            {
                //return StatusCode(ex.StatusCode, ex.Message);
                StatusMessage = ex.Message;
            }
            catch (System.Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                // Log ex.
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = Messages.Err500;
            }

        }

        public string WebApiAdapterUrlKeys { get; set; } = "";
        public string WebApiAdapterUrlMembers { get; set; } = "";

        [TempData]
        public string StatusMessage { get; set; } = "";

        [BindProperty]
        public OrgDataModel OrgData { get; set; } = new OrgDataModel();

        public class OrgDataModel
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