using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.Domain
{
    public class RepositoryOwnerApiKey : DomainEntityWithIdAudit
    {

        public int OwnerId { get; private set; }

        public string ApiKey { get; private set; }

        public DateTime ExpireOn { get; private set; }

        public bool IsRevoked { get; private set; }

        public string Comment { get; private set; }
        
        #region Methods

        public static async Task<RepositoryOwnerApiKey> Create(IQueryExecutor executor, int ownerId)
        {
            //check for existing repository owner
            var owner = await executor.ExecuteAsync(new GetRepositoryOwnerQuery(ownerId));
            if (owner == null)
                throw new Exception("RepositoryOwnerApiKey.Create: Missing Owner!");

            var key = new RepositoryOwnerApiKey(ownerId);
            await executor.Add(key);

            return key;
        }

        private RepositoryOwnerApiKey(int ownerId)
        {
            OwnerId = ownerId;
            SetApiKey();
        }

        public void SetApiKey() => ApiKey = Guid.NewGuid().ToString();
        
        public void SetExpireOn(DateTime value) => ExpireOn = value;

        public void SetComment(string value) => Comment = value;

        public void SetIsRevoked(bool value)
        {
            IsRevoked = value;

            if (IsRevoked)
                SetExpireOn(DateTime.Now);
        }

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<RepositoryOwnerApiKey>
        {
            public void Configure(EntityTypeBuilder<RepositoryOwnerApiKey> builder)
            {
                builder.ToTable("RepositoryOwnerApiKey", "dbo");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id");//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.OwnerId).IsRequired();
                builder.Property(p => p.ApiKey).HasMaxLength(128).IsRequired();
                builder.Property(p => p.Comment).HasMaxLength(512);

                builder.Property(p => p.IsRevoked).HasDefaultValue(false);
            }
        }
    }
}
