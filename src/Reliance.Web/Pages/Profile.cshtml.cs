using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Domain.Organisation;
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

        private HttpClient _client = null;
        private HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    var handler = new HttpClientHandler
                    {
                        UseDefaultCredentials = true
                    };
                    _client = new HttpClient(handler);
                    _client.BaseAddress = new Uri("https://localhost:44376/");
                }
                return _client;
            }
        }

        public async Task OnGet()
        {
            await GetOrginisations();
        }

        private async Task GetOrginisations()
        {
            try
            {
                Organisations = new List<OrgDataModel>();

                //var client = new WebClient()
                //{
                //    UseDefaultCredentials = true,
                //    Credentials = (System.Security.Principal.WindowsIdentity)HttpContext.Current.User.Identity,
                //    BaseAddress = "https://localhost:44376/"
                //};
                //var response = await client.DownloadStringTaskAsync("api/oranisations");

                //var credentials = new NetworkCredential(User.Identity.Name, User.Identity);
                //                var credentials = User.Identity;

                var response = await Client.GetAsync($"api/oranisations/{User.Identity.Name}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Organisations = JsonConvert.DeserializeObject<List<OrgDataModel>>(apiResponse);

                //var orgDtos = await Executor.CastTo<OrganisationDto>().Execute(new GetOrganisationsQuery(User.Identity.Name), o => o.Name);
                //foreach (var dto in orgDtos)
                //{
                //    Organisations.Add(
                //        new OrgDataModel()
                //        {
                //            Id = dto.Id.ToString(),
                //            Name = dto.Name,
                //            MasterEmail = dto.MasterEmail
                //        });
                //}

            }
            catch (Exception ex)
            {
                SiteLogger.LogError("Profile.GetData()", ex);
            }

        }

        private async Task GetOrginisation(string id)
        {
            try
            {
                var response = await Client.GetAsync($"api/oranisations/{id}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                OrgData = JsonConvert.DeserializeObject<OrgDataModel>(apiResponse);

                //set grid api addapter
                WebApiAdapterUrlKeys = $"/api/organisations/{OrgData.Id}/keys";
                WebApiAdapterUrlMembers = $"/api/organisations/{OrgData.Id}/members";
            }
            catch (Exception ex)
            {
                SiteLogger.LogError($"Profile.GetOrginisation({id})", ex);
            }
        }

        private async Task PostOrginisation()
        {
            try
            {
                var dto = new OrganisationDto
                {
                    Id = long.Parse(OrgData.Id),
                    Name = OrgData.Name,
                    MasterEmail = OrgData.MasterEmail
                };
                var data = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = await Client.PostAsync($"api/oranisations", data);
                string apiResponse = await response.Content.ReadAsStringAsync();
                OrgData = JsonConvert.DeserializeObject<OrgDataModel>(apiResponse);
            }
            catch (Exception ex)
            {
                SiteLogger.LogError($"Profile.PostOrginisation() OrgData.MasterEmail={OrgData.MasterEmail}", ex);
            }
        }

        private async Task PutOrginisation()
        {
            try
            {
                var dto = new OrganisationDto
                {
                    Id = long.Parse(OrgData.Id),
                    Name = OrgData.Name,
                    MasterEmail = OrgData.MasterEmail
                };
                var data = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response = await Client.PutAsync($"api/oranisations/{OrgData.Id}", data);
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                OrgData = JsonConvert.DeserializeObject<OrgDataModel>(apiResponse);
            }
            catch (Exception ex)
            {
                SiteLogger.LogError($"Profile.PutOrginisation() OrgData.Id='{OrgData.Id}'", ex);
            }
        }

        private async Task GetOrginisation()
        {
            try
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
            catch (Exception ex)
            {
                SiteLogger.LogError("Profile.GetData()", ex);
            }

        }

        public async Task OnPostProfileForm()
        {
            try
            {
                //if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
                //{
                //    ModelState.AddModelError("captcha", "Captcha validation failed");
                //    StatusMessage = "Captcha validation failed";
                //    return;
                //}
                //if (ModelState.IsValid)
                //{
                //    StatusMessage = "Success";
                //}

                //var org = await Mediator.Send(new UpdateOrganisationCommand(OrgData.Id, OrgData.Name, OrgData.MasterEmail));

                //OrgData.Id = org.Id.ToString();
                //OrgData.Name = org.Name;
                //OrgData.MasterEmail = org.MasterEmail;

                if (string.IsNullOrWhiteSpace(OrgData.Id))
                    await PostOrginisation();
                else
                    await PutOrginisation();
            }
            catch (ThisAppException ex)
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

        public async Task OnPostSelectOrganisation(string orgId)
        {
            //OrgData.Id = orgId;
            await GetOrginisation(orgId);
        }

        public string WebApiAdapterUrlKeys { get; set; } = "";
        public string WebApiAdapterUrlMembers { get; set; } = "";

        [TempData]
        public string StatusMessage { get; set; } = "";

        [BindProperty]
        public List<OrgDataModel> Organisations { get; set; }

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