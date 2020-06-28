using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Api.Organisations
{
    [Authorize]
    [RequireHttps]
    public class OrganisationKeyController : BaseController
    {
        public OrganisationKeyController(ILogger<object> logger, IQueryExecutor executor, IMediator mediator) : base(logger, executor, mediator)
        {
        }

        [HttpGet]
        [Route("api/oranisations/{oranisationId:long}/keys/")]
        //[ValidateAntiForgeryToken()]
        public async Task<IActionResult> GetPrivateKeys(long oranisationId)
        {
            try
            {
                //user id linked to org?


                //await MemberIsValid(memberId);

                //var results = new TagsDataGridDto()
                //{
                //    Items = await Executor.WithMapping<TagInfoDto>().Execute(new GetTagsQuery(memberId), o => o.TagCodeFormatted)
                //};

                ////format tag codes
                //foreach (var tag in results.Items)
                //{
                //    tag.FormatTagCode();
                //}

                var results = new OrganisationKeysDto()
                {
                    Items = new List<OrganisationKeyDto>()
                    {
                        new OrganisationKeyDto()
                        {
                            Id = 0,
                            OrganisationId = "0",
                            PrivateKey = "12344567890",
                            Description = "nothing to say ;)",
                            ExpiryDate = DateTime.Now.AddYears(1),
                            ModifyDateTime = DateTime.Now
                        }
                    }
                };

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
