﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reliance.Core.Services.Commands.Organisations;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Queries.Organisations;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using SnowStorm.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Api.Organisations
{
    [Authorize] //TODO: Setup authentication & Auhtorization
    [RequireHttps]
    //[ValidateAntiForgeryToken()]
    public class OrganisationMemberController : BaseController
    {
        public OrganisationMemberController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        [HttpGet]
        [Route("api/organisations/{organisationId:long}/members/")]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetOrgMembers(long organisationId)
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                var results = new OrganisationMembersDto()
                {
                    Items = await Executor.CastTo<OrganisationMemberDto>().Execute(new GetOrganisationMembersQuery(organisationId), o => o.Email)
                };

                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"GetOrgMembers, {ex.StatusCode}, {ex.Message}", ex);
                throw new ThisAppException(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"GetOrgMembers, {StatusCodes.Status500InternalServerError}", ex);
                throw new ThisAppException(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }

        [HttpPost]
        [Route("api/organisations/{organisationId:long}/members")]
        //[ValidateAntiForgeryToken()]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> PostOrgKey(long organisationId, [FromBody] OrganisationMemberDto data)
        {
            try
            {
                // TODO: Validate user
                // await MemberIsValid(memberId);
                // await MemberIsValidSubscriber(memberId);

                //TODO: Secure api for valid subscription

                if (organisationId != data.OrgId)
                    throw new ThisAppException(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

                var results = await Mediator.Send(new CreateOrganisationMemberCommand(data));
                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"PostOrgKey, {ex.StatusCode}, {ex.Message}", ex);
                throw new ThisAppException(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"PostOrgKey, {StatusCodes.Status500InternalServerError}", ex);
                throw new ThisAppException(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }


        [HttpPut]
        [Route("api/organisations/{organisationId:long}/members/")]
        //[ValidateAntiForgeryToken()]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> PutOrgKey(long organisationId, [FromBody] OrganisationMemberDto data)
        {
            try
            {
                // TODO: Validate user
                // await MemberIsValid(memberId);
                //await MemberIsValidSubscriber(memberId);

                //TODO: Secure api for valid subscription
                if (organisationId.ToString() != data.OrganisationId)
                    throw new ThisAppException(StatusCodes.Status401Unauthorized, Messages.Err401Unauhtorised);

                var results = await Mediator.Send(new UpdateOrganisationMemberCommand(data));
                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"PutOrgKey, {ex.StatusCode}, {ex.Message}", ex);
                throw new ThisAppException(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"PutOrgKey, {StatusCodes.Status500InternalServerError}", ex);
                throw new ThisAppException(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }


        [HttpDelete]
        [Route("api/organisations/{organisationId:long}/members/{id:long}")]
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

                var results = await Mediator.Send(new DeleteOrganisationMemberCommand(organisationId, id));
                return Ok(results);
            }
            catch (ThisAppException ex)
            {
                Logger.LogError($"DeleteOrgKey, {ex.StatusCode}, {ex.Message}", ex);
                throw new ThisAppException(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"DeleteOrgKey, {StatusCodes.Status500InternalServerError}", ex);
                throw new ThisAppException(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }

    }
}
