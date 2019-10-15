using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Threading.Tasks;

namespace Reliance.Web.Domain
{
    public class Repository : DomainEntityWithIdAudit
    {
        protected Repository() { }
        //public int Id { get; private set; }  // PK is Mapped to DomainEnitiy.Id
        public string Name { get; private set; }

        #region Methods

        public static async Task<Repository> Create(IQueryExecutor executor, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Repository.Create: Missing Name!");

            //check for existing repository
            var newRepository = await executor.ExecuteAsync(new GetRepositoryQuery(name));

            if (newRepository == null)
            {
                newRepository = new Repository(name);
                await executor.Add(newRepository);
            }

            return newRepository;
        }

        public Repository(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<Repository>
        {
            public void Configure(EntityTypeBuilder<Repository> builder)
            {
                builder.ToTable("Repository", "dbo");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id");//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.Name).HasMaxLength(1024);
            }
        }
    }
}
