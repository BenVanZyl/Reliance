using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reliance.Core.Services.Commands.Organisations;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Organisations.Commands;
using Reliance.Core.Services.Organisations.Queries;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Api.Organisations
{
    [RequireHttps]
    //[Authorize]  //TODO: Setup authentication & Auhtorization with HttpApiClinet
    //[AllowAnonymous]
    //[ApiKey]
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
        public async Task<IActionResult> PostOrganisation([FromBody] OrganisationDto organisation)
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
                if (organisationId != organisation.Id)
                    throw new ThisAppException(StatusCodes.Status409Conflict, "Organisation Id not matching.");

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

        [HttpDelete]
        [Route("api/oranisations/{organisationId:long}")]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> DeleteOrganisation(long organisationId)
        {
            try
            {
                //TODO: proper validate that this can be done.

                var results = await Mediator.Send(new DeleteOrganisationCommand(organisationId));

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
    }
}
