using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Infrastructure;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reliance.Web.ThisApp.Services.Queries;

namespace Reliance.Web.ThisApp.Data.Organisation
{
    /// <summary>
    /// Owner of all
    /// </summary>
    public class Organisation : DomainEntityWithIdWithAudit
    {
        #region Properties

        public string Name { get; private set; }

        public string MasterEmail { get; private set; }

        #endregion //properties

        #region Methods

        internal static async Task<Organisation> Create(IQueryExecutor executor, string name, string masterEmail)
        {
            //validation
            var existingValue = await executor.ExecuteAsync(new GetOrganisationQuery(name, masterEmail));
            if (existingValue != null)
                throw new ThisAppExecption(StatusCodes.Status409Conflict, Messages.Err409ObjectExists("Organisation"));

            //create new record
            var value = new Organisation(name, masterEmail);
            await executor.Add<Organisation>(value);

            //return record
            return value;
        }

        private Organisation(string name, string masterEmail)
        {
            SetName(name);
            SetMasterEmail(masterEmail);
        }

        public void SetName(string value)
        {
            if (Name != value)
                Name = value;
        }

        public void SetMasterEmail(string value)
        {
            if (MasterEmail != value)
                MasterEmail = value;
        }

        #endregion //methods

        #region Configuration

        internal class Mapping : IEntityTypeConfiguration<Organisation>
        {
            public void Configure(EntityTypeBuilder<Organisation> builder)
            {
                builder.ToTable("Organisation", "Info");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");

                builder.Property(p => p.Name).IsRequired();
                builder.Property(p => p.MasterEmail).IsRequired();

                builder.Property(p => p.Name).HasMaxLength(1024).IsRequired();
                builder.Property(p => p.MasterEmail).HasMaxLength(1024).IsRequired();

                // TODO: Setup relationship objects
            }
        }

        #endregion //config
    }
}
