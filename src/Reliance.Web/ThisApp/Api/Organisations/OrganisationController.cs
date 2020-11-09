using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Domain.Organisation;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Organisations.Commands;
using Reliance.Web.ThisApp.Services.Organisations.Queries;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Api.Organisations
{
    //[Authorize]
    [AllowAnonymous]
    [RequireHttps]
    public class OrganisationController : BaseController
    {
        public OrganisationController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        { }

        [HttpGet]
        [Route("api/oranisations/{email}")]
        //[RequirePersonalAccessToken]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetOrganisationsForEmailAddress(string email) //GetOrganisationsForLoggedInUser()
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                //if (string.IsNullOrWhiteSpace(User.Identity.Name))
                //    throw new ThisAppExecption(StatusCodes.Status403Forbidden, "Invalid user name");

                var results = await Executor.CastTo<OrganisationDto>().Execute(new GetOrganisationsQuery(email), o => o.Name);

                return Ok(results);
            }
            catch (ThisAppException ex)
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
        [Route("api/oranisations/{organisationId:long}")]
        //[RequirePersonalAccessToken]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetOrganisation(long organisationId)
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                var results = await Executor.CastTo<OrganisationDto>().Execute(new GetOrganisationQuery(organisationId));

                return Ok(results);
            }
            catch (ThisAppException ex)
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

        [HttpPost]
        [Route("api/oranisations")]
        public async Task<IActionResult> PostOrganisation([FromForm] OrganisationDto organisation)
        {
            try
            {
                var newOrganisation = await Mediator.Send(new CreateOrganisationCommand(organisation.Name, organisation.MasterEmail));
                return Ok(newOrganisation);
            }
            catch (ThisAppException ex)
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

        [HttpPut]
        [Route("api/oranisations/{organisationId:long}")]
        public async Task<IActionResult> PutOrganisation(long organisationId, [FromBody] OrganisationDto organisation)
        {
            try
            {
                var updatedOrganisation = await Mediator.Send(new UpdateOrganisationCommand(organisation.Id, organisation.Name, organisation.MasterEmail));
                return Ok(updatedOrganisation);
            }
            catch (ThisAppException ex)
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
