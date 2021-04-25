using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Organisations.Commands;
using Reliance.Core.Services.Organisations.Queries;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.Services.Commands.ApiClient;
using Reliance.Web.Services.Infrastructure;
using Reliance.Web.Services.Support;
using SnowStorm.QueryExecutors;

namespace Reliance.Web.Pages
{
    [Authorize]
    public class ConfigureModel : BasePageModel
    {
        public ConfigureModel(ILogger<object> logger, IQueryExecutor executor, IMediator mediator, IApiClient apiClient) : base(logger, executor, mediator)
        {
            _apiClient = apiClient;
        }

        private IApiClient _apiClient;

        #region public properties

        [TempData]
        public string StatusMessage { get; set; } = "";

        [BindProperty]
        public OrganisationDto OrgData { get; set; } = new OrganisationDto();


        #endregion

        #region events
        public async Task OnGet()
        {
            await GetOrginisations();
        }

        public async Task OnPostSaveOrganisationDirect()
        {
            await SaveOrginisationDirect();
        }

        public async Task OnPostSaveOrganisationApiClient()
        {
            await SaveOrganisationApiClient();
        }

        #endregion

        #region private methods

        private async Task GetOrginisations()
        {
            try
            {
                var Organisations = new List<OrganisationDto>();
                var email = User.Identity.Name;

                Organisations = await Executor.CastTo<OrganisationDto>().Execute(new GetOrganisationsQuery(email), o => o.Name);

                if (Organisations == null || Organisations.Count == 0)
                {
                    //create a new one
                    OrgData = await Mediator.Send(new CreateOrganisationCommand(email, email));
                    StatusMessage = "Profile created.  Please update as required";
                    return;
                }

                OrgData = Organisations.First();

                //TODO: functionality to associate this user with multiple organisations.
            }
            catch (ThisAppException ex)
            {
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = $"Listing Organisations failed: {ex.Message}";
            }
            catch (System.Exception ex)
            {
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = $"Listing Organisations failed: {Messages.Err500}";
            }
        }

        private async Task SaveOrginisationDirect()
        {
            try
            {
                OrgData = await Mediator.Send(new UpdateOrganisationCommand(OrgData.Id, OrgData.Name, OrgData.MasterEmail));

                StatusMessage = "Organisation Saved";
            }
            catch (ThisAppException ex)
            {
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = $"Save Organisation failed: {ex.Message}";
            }
            catch (System.Exception ex)
            {
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = $"Save Organisation failed: {Messages.Err500}";
            }
            //finally
            //{
            //      TODO: functionality to associate this user with multiple organisations.
            //    await GetOrginisations();
            //}
        }


        private async Task SaveOrganisationApiClient()
        {
            try
            {
                var dto = new OrganisationDto
                {
                    Id = OrgData.Id,
                    Name = OrgData.Name,
                    MasterEmail = OrgData.MasterEmail
                };

                var results = await Mediator.Send(new ExecuteCommand(ExecuteCommand.HttpAction.Put, $"api/oranisations/{dto.Id}", dto));

                OrgData = JsonConvert.DeserializeObject<OrganisationDto>(results);
                StatusMessage = "Organisation Saved"; 

            }
            catch (ThisAppException ex)
            {
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = $"Save Organisation failed: {ex.Message}";
            }
            catch (System.Exception ex)
            {
                SiteLogger.LogError(this.ToString(), ex);
                StatusMessage = $"Save Organisation failed: {Messages.Err500}";
            }
            //finally
            //{
            //      TODO: functionality to associate this user with multiple organisations.
            //    await GetOrginisations();
            //}
        }
        #endregion
    }
}
