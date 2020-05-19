using MediatR;
using Reliance.Web.Client.Api;
using Reliance.Web.Services.Commands;
using Reliance.Web.Services.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnowStorm.Infrastructure.QueryExecutors;

namespace Reliance.Web.Services.Repositories
{
    public class MasterRepository: IMasterRepository
    {
        private readonly IQueryExecutor _executor;
        private readonly IMediator _mediator;

        public MasterRepository(IQueryExecutor executor, IMediator mediator)
        {
            _executor = executor;
            _mediator = mediator;
        }

        public async Task<List<PackageDto>> GetPackages()
        {
            var results = await _executor.WithMapping<PackageDto>()
                .ExecuteAsync(new GetPackagesQuery(), o => o.Name);
            return results;
        }

        public async Task<List<PackageDto>> GetPackages(string packageName)
        {
            var results = await _executor.WithMapping<PackageDto>()
                .ExecuteAsync(new GetPackagesQuery(packageName), o => o.Name);
            return results;
        }

        public async Task<List<string>> GetPackageNames()
        {
            var source = await _executor.WithMapping<PackageDto>()
                .ExecuteAsync(new GetPackagesQuery(), o => o.Name);

            var results = source.Select(s => s.Name).Distinct().ToList();

            return results;
        }

        public async Task<PackageDto> GetPackage(string packageId)
        {
            long.TryParse(packageId, out long id);
            if (id <= 0)
                throw new Exception("Invalid id for package!");

            return await GetPackage(id);
        }

        public async Task<PackageDto> GetPackage(long packageId)
        {
            var results = await _executor.WithMapping<PackageDto>()
                .ExecuteAsync(new GetPackageQuery(packageId));
            return results;
        }

        public async Task<RepositoryDto> GetRepository(string repositoryId)
        {
            long.TryParse(repositoryId, out long id);
            if (id <= 0)
                throw new Exception("Invalid id for repository!");

            return await GetRepository(id);
        }

        public async Task<RepositoryDto> GetRepository(long repositoryId)
        {
            var results = await _executor.WithMapping<RepositoryDto>()
               .ExecuteAsync(new GetRepositoryQuery(repositoryId));
            return results;
        }

        public async Task<List<RepositoryDto>> GetRepositories()
        {
            var results = await _executor.WithMapping<RepositoryDto>()
                .ExecuteAsync(new GetRepositoriesQuery(1), o => o.Name); //TODO: Implement getting OwnerId for all queries
            return results;
        }

        public async Task<List<RepositoryDto>> GetRepositories(string packageId)
        {
            long.TryParse(packageId, out long id);
            if (id <= 0)
                throw new Exception("Invalid package id for repositories!");

            return await GetRepositories(id);
        }

        public async Task<List<RepositoryDto>> GetRepositories(long packageId)
        {
            var results = await _executor.WithMapping<RepositoryDto>()
                .ExecuteAsync(new GetRepositoriesQuery(packageId), o => o.Name);
            return results;
        }

    }
}
