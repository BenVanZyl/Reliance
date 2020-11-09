using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries.DevOps;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Domain.DevOps
{
    public class Badge : DomainEntityWithIdWithAudit
    {
        #region Properties
        public long AppId { get; set; }

        [ForeignKey("AppId")]
        public App App { get; set; }

        public long StageId { get; set; }

        [ForeignKey("StageId")]
        public Stage Stage { get; set; }

        public string BadgeUrl { get; set; }

        #endregion //properties

        #region Methods
        internal static async Task<Badge> Create(IQueryExecutor executor, long appId, long stageId, string badgeUrl)
        {
            //validation - confirm app does not already exists
            var existingValue = await executor.Execute(new GetBadgeQuery(appId, stageId));
            if (existingValue != null)
                throw new ThisAppException(StatusCodes.Status409Conflict, Messages.Err409ObjectExists("Badge"));
            //create new record
            var value = new Badge(appId, stageId, badgeUrl);
            await executor.Add<Badge>(value);
            await executor.Save();
            //return record
            return value;
        }
        private Badge(long appId, long stageId, string badgeUrl)
        {
            AppId = appId;      // can only be set at creation
            StageId = stageId;  // can only be set at creation
            SetBadgeUrl(badgeUrl);
        }
        public void SetBadgeUrl(string value)
        {
            if (BadgeUrl != value)
                BadgeUrl = value;
        }
        #endregion //methods
        #region Configuration
        internal class Mapping : IEntityTypeConfiguration<Badge>
        {
            public void Configure(EntityTypeBuilder<Badge> builder)
            {
                builder.ToTable("Badge", "DevOps");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");
                builder.Property(p => p.AppId).IsRequired();
                builder.Property(p => p.StageId).IsRequired();
                // TODO: Setup relationship objects
            }
        }
        #endregion //config
    }
}
