using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reliance.Web.Client.Api;
using Reliance.Web.Domain;
using Reliance.Web.Services.Queries;
using Reliance.Web.Services.Repositories;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reliance.Web.Pages
{
    public class PackagesModel : PageModel
    {       
        public List<string> PackageNames { get; set; }
        public string SelectedPackageName { get; set; }

        public List<PackageDto> Packages { get; set; }
        public string SelectedPackageId { get; set; }
        public PackageDto SelectedPackage { get; private set; }

        public List<Repository> PackageRepositories { get; set; }
        

        private MasterRepository _master;

        public PackagesModel(IMasterRepository master)
        {
            _master = (MasterRepository)master;
        }

        public async Task OnGet()
        {
            await GetPageDataAsync();
        }

        //public async Task OnPostPackageNameSelected(string packageName)
        //{
        //    SelectedPackageName = packageName;
        //    await GetPageDataAsync();
        //    await GetPackages(packageName);
        //}

        public async Task OnPostPackageNameSelected(string packageName, string packageId)
        {

            SelectedPackageName = packageName;

            if (!string.IsNullOrEmpty(packageId))
            {
                SelectedPackageId = packageId;
                await GetPackage(packageId);
            }

            await GetPageDataAsync();
            await GetPackages(packageName);
        }

        public async Task OnPostPackageSelected(string packageId)
        {
            SelectedPackageId = packageId;
            await GetPackage(packageId);
            await GetPageDataAsync();
            await GetPackages(SelectedPackageName);
        }


        public SelectList PackageNamesSelectList()
        {
            var sl = new SelectList(PackageNamesListBox());
            return sl;
        }

        public List<SelectListItem> PackageNamesListBox()
        {
            var lst = new List<SelectListItem>();

            foreach (var pn in PackageNames)
            {
                lst.Add(new SelectListItem() { Text = pn, Value = pn, Selected = pn == SelectedPackageName ? true : false });
            }

            return lst;

        }

        private async Task GetPageDataAsync()
        {
            PackageNames = await _master.GetPackageNames();
        }

        private async Task GetPackages(string packageName)
        {
            Packages = await _master.GetPackages(SelectedPackageName);
        }
        
        private async Task GetPackage(string packageId)
        {
            SelectedPackage = await _master.GetPackage(packageId);
        }

    }
}