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
    public class Repository : DomainEntityWithIdWithAudit
    {
        protected Repository() { }
        
        // Id PK is Mapped to DomainEnitiy.Id
        public string Name { get; private set; }
        public long OwnerId { get; private set; }

        [ForeignKey("OwnerId")]
        public RepositoryOwner Owner { get; private set; }

        #region Methods

        public static async Task<Repository> Create(IQueryExecutor executor, string name, long ownerId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Repository.Create: Missing Name!");

            //confirm ownerId
            var owner = await executor.ExecuteAsync(new GetRepositoryOwnerQuery(ownerId));
            if (owner == null)
                throw new Exception("Repository.Create: Missing Owner!");

            //check for existing repository
            var newRepository = await executor.ExecuteAsync(new GetRepositoryQuery(name, ownerId));

            if (newRepository == null)
            {
                newRepository = new Repository(name, owner);
                await executor.Add(newRepository);
            }

            return newRepository;
        }

        public Repository(string name, RepositoryOwner owner)
        {
            Name = name;
            Owner = owner;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetOwnerId(long ownerId)
        {
            OwnerId = ownerId;
        }

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<Repository>
        {
            public void Configure(EntityTypeBuilder<Repository> builder)
            {
                builder.ToTable("Repository", "Reliance");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id");//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.OwnerId).IsRequired();
                builder.Property(p => p.Name).HasMaxLength(1024).IsRequired();

            }
        }
    }
}
