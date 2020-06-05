using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Reliance.Web.Domain
{
    public class Solution : DomainEntityWithIdWithAudit
    {
        protected Solution() { }

        // Id PK is Mapped to DomainEnitiy.Id
        public string Name { get; private set; }
        public long RepositoryId { get; private set; }  // TODO: Foreign Key Column requires config.

        [ForeignKey("RepositoryId")]
        public Repository Repository { get; private set; }

        #region Methods

        public static async Task<Solution> Create(IQueryExecutor executor, string name, long repositoryId)
        {
            var repository = await executor.ExecuteAsync(new GetRepositoryQuery(repositoryId));
            if (repository == null)
                throw new Exception("Solution.Create: Repository not found");

            return await Create(executor, name, repository);
        }

        public static async Task<Solution> Create(IQueryExecutor executor, string name, Repository repository)
        {
            Solution newSolution = null;
            newSolution = await executor.ExecuteAsync(new GetSolutionQuery(name, repository.Id));

            if (newSolution == null)
            {
                newSolution = new Solution(name, repository);
                await executor.Add(newSolution);
            }
            return newSolution;
        }

        public Solution(string name, Repository repository)
        {
            Update(name, repository);
        }

        public void Update(string name, Repository repository)
        {
            Name = name;
            Repository = repository;
        }

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<Solution>
        {
            public void Configure(EntityTypeBuilder<Solution> builder)
            {
                builder.ToTable("Solution", "Reliance");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.Name).HasMaxLength(1024);

                //Parent
                builder.HasOne<Repository>().WithOne().HasForeignKey<Solution>(e => e.RepositoryId);

            }
        }
    }
}
