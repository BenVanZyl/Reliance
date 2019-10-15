using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Threading.Tasks;

namespace Reliance.Web.Domain
{
    public class ProjectPackage : DomainEntityWithIdAudit
    {
        protected ProjectPackage() { }
        //public int Id { get; private set; }  // PK is Mapped to DomainEnitiy.Id
        public int ProjectId { get; private set; }  // TODO: Foreign Key Column requires config.
        public int PackageId { get; private set; }  // TODO: Foreign Key Column requires config.

        public Project Project { get; private set; }
        public Package Package { get; private set; }

        #region Methods

        public static async Task<ProjectPackage> Create(IQueryExecutor executor, int projectId, int packageId)
        {
            var project = await executor.ExecuteAsync(new GetProjectQuery(projectId));
            if (project == null)
                throw new Exception("ProjectPackage.Create: Package not found!");

            var package = await executor.ExecuteAsync(new GetPackageQuery(packageId));
            if (package == null)
                throw new Exception("ProjectPackage.Create: Package not found!");

            var newProjectPackage = await executor.ExecuteAsync(new GetProjectPackageQuery(projectId, packageId));
            if (newProjectPackage == null)
            {
                newProjectPackage = new ProjectPackage(project, package);
                await executor.Add(newProjectPackage);
            }
            return newProjectPackage;
        }

        public ProjectPackage(Project project, Package package)
        {
            Update(project, package);
        }

        public void Update(Project project, Package package)
        {
            Project = project;
            Package = package;
        }


        #endregion

        internal class EntityMap : IEntityTypeConfiguration<ProjectPackage>
        {
            public void Configure(EntityTypeBuilder<ProjectPackage> builder)
            {
                builder.ToTable("ProjectPackage", "dbo");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id"); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.HasOne<Project>().WithOne().HasForeignKey<ProjectPackage>(e => e.ProjectId);
                builder.HasOne<Package>().WithOne().HasForeignKey<ProjectPackage>(e => e.PackageId);
            }
        }
    }
}
