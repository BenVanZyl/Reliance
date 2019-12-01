using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reliance.Web.Client.Api;
using Reliance.Web.Domain;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.QueryExecutors;

namespace Reliance.Web.Pages
{
    public class RepoOwnerModel : PageModel
    {
        public RepositoryOwnerDto OwnerInfo { get; set; }

        private readonly IQueryExecutor _executor;
        private readonly IMediator _mediator;

        public RepoOwnerModel(IQueryExecutor executor, IMediator mediator)
        {
            _executor = executor;
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            //must check user is authenticated
            ValidateUser();

            OwnerInfo = await _executor.ExecuteAsync(new GetRepositoryOwnerQuery(User.Identity.Name));
            //get owner id

        }

        public void OnPostUpdateOwnerInfo()
        {
            ValidateUser();
        }

        public void OnPostNewApiKey(string RepositoryOwnerId)
        {
            ValidateUser();
        }

        private void ValidateUser()
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/Identity/Account/Login");
        }
    }
}