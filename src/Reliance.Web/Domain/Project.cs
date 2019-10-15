using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Threading.Tasks;

namespace Reliance.Web.Domain
{
    public class Project: DomainEntityWithIdAudit
    {
        protected Project() { }
        
        //public int Id { get; private set; }  // PK is Mapped to DomainEnitiy.Id
        public string Name { get; private set; }
        public int SolutionId { get; private set; }  // TODO: Foreign Key Column requires config.
        public Solution Solution { get; private set; }

        #region Methods

        public static async Task<Project> Create(IQueryExecutor executor, string name, int solutionId)
        {
            var solution = await executor.ExecuteAsync(new GetSolutionQuery(solutionId));
            if (solution == null)
                throw new Exception("Project.Create: Solution not found!");

            return await Create(executor, name, solution);
        }

        public static async Task<Project> Create(IQueryExecutor executor, string name, Solution solution)
        {
            Project newProject = null;
            newProject = await executor.ExecuteAsync(new GetProjectQuery(name, solution.Id));

            if (newProject == null)
            {
                newProject = new Project(name, solution);
                await executor.Add(newProject);
            }
            return newProject;
        }

        public Project(string name, Solution solution)
        {
            Update(name, Solution);
        }

        public void Update(string name, Solution solution)
        {
            Name = name;
            Solution = solution;
        }

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<Project>
        {
            public void Configure(EntityTypeBuilder<Project> builder)
            {
                builder.ToTable("Project", "dbo");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id"); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.Name).HasMaxLength(255);

                builder.HasOne<Solution>().WithOne().HasForeignKey<Project>(e => e.SolutionId);
            }
        }
    }
}
