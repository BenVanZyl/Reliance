using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Threading.Tasks;

namespace Reliance.Web.Domain
{
    //TODO: New feature to be implemented.
    public class RepositoryOwner: DomainEntityWithIdWithAudit
    {
        protected RepositoryOwner() { }

        public string UserId { get; private set; }
        public string Name { get; private set; }

        public bool IsOrganisation { get; private set; }

        public string Description { get; private set; }

        #region Methods

        public static async Task<RepositoryOwner> Create(IQueryExecutor executor, string userId, string ownerName, bool isOrganisation = false, string Description = "")
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new Exception("RepositoryOwner.Create: Missing UserId!");

            //check for existing repository owner
            var owner = await executor.ExecuteAsync(new GetRepositoryOwnerQuery(userId));
            if (owner == null)
            {
                owner = new RepositoryOwner(userId, Description);
                await executor.Add(owner);
            }

            return owner;
        }

        public RepositoryOwner(string userId, string ownerName, bool isOrganisation = false, string description = "")
        {
            UserId = userId;
            Name = ownerName;
            IsOrganisation = isOrganisation;
            Description = description;
        }
        
        public void SetOwnerName(string value) => Name = value;

        public void SetIsOrganisation(bool value) => IsOrganisation = value;

        public void SetDescription(string value) => Description = value;

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<RepositoryOwner>
        {
            public void Configure(EntityTypeBuilder<RepositoryOwner> builder)
            {
                builder.ToTable("RepositoryOwner", "Reliance");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id");//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.UserId).HasMaxLength(450);
                builder.Property(p => p.Name).HasMaxLength(128);
                builder.Property(p => p.Description).HasMaxLength(2048);

                builder.Property(p => p.IsOrganisation).HasDefaultValue(false);
            }
        }
    }
}
