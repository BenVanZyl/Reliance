﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Organisations.Queries;
using Reliance.Web.Client;
using SnowStorm.Domain;
using SnowStorm.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Domain.Organisation
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
            var existingValue = await executor.Execute(new GetOrganisationQuery(name, masterEmail));
            if (existingValue != null)
                throw new ThisAppException(StatusCodes.Status409Conflict, Messages.Err409ObjectExists("Organisation"));

            //create new record
            var value = new Organisation(name, masterEmail);
            await executor.Add<Organisation>(value);
            await executor.Save();

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

                builder.Property(p => p.Name).HasMaxLength(1024).IsRequired();
                builder.Property(p => p.MasterEmail).HasMaxLength(1024).IsRequired();

                // TODO: Setup relationship objects
            }
        }

        #endregion //config
    }
}
