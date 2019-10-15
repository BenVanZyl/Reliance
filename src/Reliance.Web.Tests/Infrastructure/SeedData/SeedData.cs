using Reliance.Web.Client.Api;
using Reliance.Web.Domain;
using SnowStorm.Infrastructure.Domain;
using System.Linq;

namespace Reliance.Test.Infrastructure.SeedData
{
    public static class SeedData
    {
        public static void Build(AppDbContext dbContext)
        {
            dbContext.Set<Repository>().Add(new Repository("Repository1"));
            var r1 = dbContext.Set<Repository>().FirstOrDefault(w => w.Name == "Repository1");

            dbContext.Set<Solution>().Add(new Solution("Solution1", r1));
            var s1 = dbContext.Set<Solution>().FirstOrDefault(w => w.Name == "Solution1" && w.RepositoryId == r1.Id);

            dbContext.Set<Project>().Add(new Project("Project1", s1));
            var p1 = dbContext.Set<Project>().FirstOrDefault(w => w.Name == "Project1" && w.SolutionId == s1.Id);

            dbContext.Set<Package>().Add(new Package(GetPackageDto("Package1")));
            var pkg1 = dbContext.Set<Package>().FirstOrDefault(w => w.Name == "Package1");
            
            dbContext.Set<ProjectPackage>().Add(new ProjectPackage(p1, pkg1));
            var prjPkg1 = dbContext.Set<ProjectPackage>().FirstOrDefault(w => w.Project == p1 && w.Package == pkg1);


            dbContext.SaveChanges();
        }

        private static PackageDto GetPackageDto(string name)
        {
            return new PackageDto()
            {
                Name = name,
                VersionMaster = 1,
                VersionMinor = 0,
                VersionPatch = 0,
                TargetFrameWork = "net461"
            };
        }
    }
}
