using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reliance.Web.Client.Api;
using Reliance.Web.Services.Commands;
using Reliance.Web.Services.Queries;
using Reliance.Web.Services.Repositories;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reliance.Web.Api
{
    [Route("api")]
    public class RelianceController : Controller
    {
        private readonly IQueryExecutor _executor;
        private readonly IMediator _mediator;
        private readonly MasterRepository _master;

        

        public RelianceController(IQueryExecutor executor, IMediator mediator, IMasterRepository master)
        {
            _executor = executor;
            _mediator = mediator;
            _master = (MasterRepository) master;

            
        }

        [HttpGet]
        [Route("api/reliance/repositories")]
        [ProducesDefaultResponseType(typeof(List<RepositoryDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetRepositories()
        {
            //var results = await _executor.WithMapping<RepositoryDto>().Execute(new GetRepositoriesQuery(), o => o.Name);
            var results = await _master.GetRepositories();
            return Ok(results);
        }

        [HttpGet]
        [Route("api/reliance/repositories/{repositoryId:long}/solutions")]
        [ProducesDefaultResponseType(typeof(List<SolutionDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetSolutions([FromQuery] long repositoryId)
        {
            var results = await _executor.WithMapping<SolutionDto>().Execute(new GetSolutionsQuery(repositoryId), o => o.Name);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("api/reliance/repositories/solutions/{solutionId:long}/projects")]
        [ProducesDefaultResponseType(typeof(List<ProjectDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetSolutionProjects([FromQuery] long solutionId)
        {
            var results = await _executor.WithMapping<ProjectDto>().Execute(new GetProjectsQuery(solutionId), o => o.Name);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/reliance/packages")]
        [ProducesDefaultResponseType(typeof(List<PackageDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetPackages()
        {
            var results = await _executor.WithMapping<PackageDto>().Execute(new GetPackagesQuery(), o => o.Name);
            return Ok(results);
        }

        //################################################

        [HttpPost]
        [Route("api/reliance/repositories")]
        [ProducesDefaultResponseType(typeof(List<RepositoryDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> PostRepositories([FromBody] PostRepositoryDetailsDto data)
        {
            var results = await _mediator.Send(new PostRepositryDetailsCommand(data)); //_executor.WithMapping<RepositoryDto>().Execute(new GetRepositoriesQuery(), o => o.Name);
            return Ok(results);
        }
    }
}
