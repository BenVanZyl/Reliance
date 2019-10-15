using Microsoft.AspNetCore.Mvc.RazorPages;
using Reliance.Web.Client.Api;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reliance.Web.Pages
{
    public class PackagesModel : PageModel
    {
        public List<PackageDto> Packages { get; set; }

        public string SelectedPackage { get; set; }

        private QueryExecutor _executor;

        public PackagesModel(IQueryExecutor executor)
        {
            _executor = (QueryExecutor)executor;
        }

        public async Task OnGet()
        {
            await GetPageDataAsync();
        }

        private async Task GetPageDataAsync()
        {
            Packages = await _executor.WithMapping<PackageDto>().ExecuteAsync(new GetPackagesQuery(), o => o);
        }
    }
}