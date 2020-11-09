using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Client;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Queries.DevOps;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Domain.DevOps
{
    public class Stage : ReferenceType
    {
        #region Properties
        public long OrderBy { get; private set; }
        #endregion //properties
        #region Methods
        internal static async Task<Stage> Create(IQueryExecutor executor, long organisationId, string name, long orderBy = 0)
        {
            //validation - confirm app does not already exists
            if (string.IsNullOrWhiteSpace(name))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("Stage"));
            var existingValue = await executor.Execute(new GetStageQuery(organisationId, name));
            if (existingValue != null)
                throw new ThisAppException(StatusCodes.Status409Conflict, Messages.Err409ObjectExists("Stage"));
            //create new record
            var value = new Stage(organisationId, name, orderBy);
            await executor.Add<Stage>(value);
            await executor.Save();
            //return record
            return value;
        }
        private Stage(long organisationId, string name, long orderBy) : base(organisationId, name)
        {
            SetOrderBy(orderBy);
        }
        public void SetOrderBy(long value)
        {
            if (OrderBy != value)
                OrderBy = value;
        }
        #endregion //methods
        #region Configuration
        internal class Mapping : IEntityTypeConfiguration<Stage>
        {
            public void Configure(EntityTypeBuilder<Stage> builder)
            {
                builder.ToTable("Stage", "DevOps");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");
                builder.Property(p => p.Name).IsRequired();
                builder.Property(p => p.Name).HasMaxLength(1024).IsRequired();
                // TODO: Setup relationship objects
            }
        }
        #endregion //config
    }
}
