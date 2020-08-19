using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reliance.Web.ThisApp.Services.ActionFilters;
using Reliance.Web.ThisApp.Services.Organisations.Queries;
using Reliance.Web.ThisApp.Infrastructure;
using Microsoft.AspNetCore.Http;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Microsoft.AspNetCore.Authorization;

namespace Reliance.Web.ThisApp.Api.Organisations
{
    [Authorize]
    [RequireHttps]
    public class OrganisationController : BaseController
    {
        public OrganisationController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        [HttpGet]
        [Route("api/oranisations/{organisationId:long}")]
        //[RequirePersonalAccessToken]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetOrganisation(long organisationId)
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                var results = await Executor.WithMapping<OrganisationDto>().Execute(new GetOrganisationQuery(organisationId));

                return Ok(results);
            }
            catch (ThisAppExecption ex)
            {
                Logger.LogError($"GetOrganisationsForLoggedInUser, {ex.StatusCode}, {ex.Message}", ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"GetOrganisationsForLoggedInUser, {StatusCodes.Status500InternalServerError}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }

        [HttpGet]
        [Route("api/oranisations")]
        //[RequirePersonalAccessToken]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetOrganisationsForLoggedInUser()
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                if (string.IsNullOrWhiteSpace(User.Identity.Name))
                    throw new ThisAppExecption(StatusCodes.Status403Forbidden, "Invalid user name");

                var results = await Executor.WithMapping<OrganisationDto>().Execute(new GetOrganisationsQuery(User.Identity.Name), o => o.Name);

                return Ok(results);
            }
            catch (ThisAppExecption ex)
            {
                Logger.LogError($"GetOrganisationsForLoggedInUser, {ex.StatusCode}, {ex.Message}", ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"GetOrganisationsForLoggedInUser, {StatusCodes.Status500InternalServerError}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }

    }
}
