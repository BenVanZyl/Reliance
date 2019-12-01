using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reliance.Web.Client.Api;
using Reliance.Web.Services.Repositories;

namespace Reliance.Web.Pages
{
    public class ProjectsModel : PageModel
    {
        public string SelectedRepositoryId { get; private set; }
        public RepositoryDto SelectedRepository { get; private set; }

        public List<RepositoryDto> Repositories { get; private set; }
        public List<ProjectDto> Projects { get; private set; }


        private MasterRepository _master;

        public ProjectsModel(IMasterRepository master)
        {
            _master = (MasterRepository)master;
        }

        public async Task OnGetAsync()
        {
            await GetPageDataAsync();
        }

        private async Task GetPageDataAsync()
        {
            Repositories = await _master.GetRepositories();
        }
    }
}