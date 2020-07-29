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

namespace Reliance.Web.ThisApp.Api.Organisations
{
    [RequireHttps]
    public class OrganisationController : BaseController
    {
        public OrganisationController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        [HttpGet]
        [Route("api/oranisations/{organisationId:long}")]
        [RequirePersonalAccessTokenAttribute]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetOrganisation(long organisationId)
        {
            try
            {
                //TODO: user id linked to org?
                //await MemberIsValid(memberId);

                var results = await Executor.Execute(new GetOrganisationsQuery(User.Identity.Name));

                return Ok(results);
            }
            catch (ThisAppExecption ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                //log ex
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.Err500);
            }
        }

    }
}
