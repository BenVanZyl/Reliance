using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Client;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries.Organisations;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Domain.Organisation
{
    public class OrganisationKey : DomainEntityWithIdWithAudit
    {
        #region Properties

        public long OrganisationId { get; private set; }
        public string PrivateKey { get; private set; }
        public string Description { get; private set; }
        public DateTime ExpiryDate { get; private set; }

        [ForeignKey("OrganisationId")]
        public Organisation Organisation { get; private set; }

        #endregion //properties

        #region Methods

        internal static async Task<OrganisationKey> Create(IQueryExecutor executor, OrganisationKeyDto data)
        {
            //validate inputs
            if (!long.TryParse(data.OrganisationId, out long orgId))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation Id"));
            if (orgId == 0)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectId("Organisation Id"));
            if (!string.IsNullOrWhiteSpace(data.PrivateKey))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectData("Private Key can only be created by the system."));

            if (!data.ExpiryDate.HasValue)
                data.ExpiryDate = DateTime.Now.AddYears(3);

            //check for duplicates
            var duplicates = await executor.Execute(new GetOrganisationKeysQuery(data));
            if (duplicates != null && duplicates.Count > 0)
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417InvalidObjectData("Duplicate Private Key records not allowed."));

            //create the record
            var value = new OrganisationKey(orgId);
            value.SetDescription(data.Description);
            value.SetExpiryDate(data.ExpiryDate.Value);

            await executor.Add<OrganisationKey>(value);

            await executor.Save();

            return value;
        }

        private OrganisationKey(long organisationId)
        {
            OrganisationId = organisationId;
            PrivateKey = Guid.NewGuid().ToString();  // can only be set at creation
        }

        public void SetDescription(string value)
        {
            if (Description != value)
                Description = value;
        }

        public void SetExpiryDate(DateTime value)
        {
            if (ExpiryDate!= value)
                ExpiryDate = value;
        }

        #endregion //methods

        #region Configuration

        internal class Mapping : IEntityTypeConfiguration<OrganisationKey>
        {
            public void Configure(EntityTypeBuilder<OrganisationKey> builder)
            {
                builder.ToTable("OrganisationKey", "Info");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");

                builder.Property(p => p.OrganisationId).IsRequired();
                builder.Property(p => p.PrivateKey).IsRequired();

                builder.Property(p => p.PrivateKey).HasMaxLength(36).IsRequired();
                builder.Property(p => p.Description).HasMaxLength(256).IsRequired();

                // TODO: Setup relationship objects
            }
        }

        #endregion //config//config
    }
}
