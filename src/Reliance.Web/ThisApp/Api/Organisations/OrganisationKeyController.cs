using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Commands.Organisations;
using Reliance.Web.ThisApp.Services.Queries.Organisations;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Api.Organisations
{
    [Authorize]
    [RequireHttps]
    //[ValidateAntiForgeryToken()]
    public class OrganisationKeyController : BaseController
    {
        public OrganisationKeyController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        [HttpGet]
        [Route("api/organisations/{organisationId:long}/keys/")]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetPrivateKeys(long organisationId)
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                var results = new OrganisationKeysDto()
                {
                    Items = await Executor.CastTo<OrganisationKeyDto>().Execute(new GetOrganisationKeysQuery(organisationId))
                };

                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"GetPrivateKeys, {ex.StatusCode}, {ex.Message}", ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"GetPrivateKeys, {StatusCodes.Status500InternalServerError}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }

        [HttpPost]
        [Route("api/organisations/{organisationId:long}/keys/")]
        //[ValidateAntiForgeryToken()]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> PostOrgKey(long organisationId, [FromBody] OrganisationKeyDto data)
        {
            try
            {
                // TODO: Validate user
                // await MemberIsValid(memberId);
                // await MemberIsValidSubscriber(memberId);

                //TODO: Secure api for valid subscription

                if (organisationId.ToString() != data.OrganisationId)
                    throw new ThisAppException(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

                var results = await Mediator.Send(new CreateOrganisationKeyCommand(data));
                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"PostOrgKey, {ex.StatusCode}, {ex.Message}", ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"PostOrgKey, {StatusCodes.Status500InternalServerError}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }

        }


        [HttpPut]
        [Route("api/organisations/{organisationId:long}/keys/")]
        //[ValidateAntiForgeryToken()]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> PutOrgKey(long organisationId, [FromBody] OrganisationKeyDto data)
        {
            try
            {
                // TODO: Validate user
                // await MemberIsValid(memberId);
                //await MemberIsValidSubscriber(memberId);

                //TODO: Secure api for valid subscription
                if (organisationId.ToString() != data.OrganisationId)
                    throw new ThisAppException(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

                var results = await Mediator.Send(new UpdateOrganisationKeyCommand(data));
                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"PutOrgKey, {ex.StatusCode}, {ex.Message}", ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"PutOrgKey, {StatusCodes.Status500InternalServerError}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }

        }


        [HttpDelete]
        [Route("api/organisations/{organisationId:long}/keys/{id:long}")]
        //[ValidateAntiForgeryToken()]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> DeleteOrgKey(long organisationId, long id)
        {
            try
            {
                // TODO: Validae user
                // await MemberIsValid(memberId);
                // await MemberIsValidSubscriber(memberId);

                //TODO: Secure api for valid subscription

                var results = await Mediator.Send(new DeleteOrganisationKeyCommand(organisationId, id));
                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"DeleteOrgKey, {ex.StatusCode}, {ex.Message}", ex);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"DeleteOrgKey, {StatusCodes.Status500InternalServerError}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }
    }
}
