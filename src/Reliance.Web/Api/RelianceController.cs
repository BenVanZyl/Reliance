using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reliance.Web.Client.Api;
using Reliance.Web.Services.Commands;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reliance.Web.Api
{
    public class RelianceController : Controller
    {
        private readonly IQueryExecutor _executor;
        private readonly IMediator _mediator;

        public RelianceController(IQueryExecutor executor, IMediator mediator)
        {
            _executor = executor;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/reliance/repositories")]
        [ProducesDefaultResponseType(typeof(List<RepositoryDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetRepositories()
        {
            var results = await _executor.WithMapping<RepositoryDto>().ExecuteAsync(new GetRepositoriesQuery(), o => o.Name);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/reliance/repositories/{repositoryId:int}/solutions")]
        [ProducesDefaultResponseType(typeof(List<SolutionDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetSolutions(int repositoryId)
        {
            var results = await _executor.WithMapping<SolutionDto>().ExecuteAsync(new GetSolutionsQuery(repositoryId), o => o.Name);
            return Ok(results);
        }
        
        [HttpGet]
        [Route("api/reliance/repositories/solutions/{solutionId:int}/projects")]
        [ProducesDefaultResponseType(typeof(List<ProjectDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetSolutionProjects(int solutionId)
        {
            var results = await _executor.WithMapping<ProjectDto>().ExecuteAsync(new GetProjectsQuery(solutionId), o => o.Name);
            return Ok(results);
        }

        [HttpGet]
        [Route("api/reliance/packages")]
        [ProducesDefaultResponseType(typeof(List<PackageDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> GetPackages()
        {
            var results = await _executor.WithMapping<PackageDto>().ExecuteAsync(new GetPackagesQuery(), o => o.Name);
            return Ok(results);
        }

        //################################################

        [HttpPost]
        [Route("api/reliance/repositories")]
        [ProducesDefaultResponseType(typeof(List<RepositoryDto>))]
        //[RequiresPermission(ApplicationType., PermissionType.Admin)]
        public async Task<IActionResult> PostRepositories([FromBody] PostRepositoryDetailsDto data)
        {
            var results = await _mediator.Send(new PostRepositryDetailsCommand(data)); //_executor.WithMapping<RepositoryDto>().ExecuteAsync(new GetRepositoriesQuery(), o => o.Name);
            return Ok(results);
        }
    }
}
