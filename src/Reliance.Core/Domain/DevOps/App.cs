using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Core.Infrastructure;
using Reliance.Core.Services.Infrastructure;
using Reliance.Core.Services.Queries.DevOps;
using Reliance.Web.Client;
using SnowStorm.QueryExecutors;
using System.Threading.Tasks;

namespace Reliance.Core.Services.Domain.DevOps
{
    public class App : ReferenceType
    {
        #region Properties
        #endregion //properties
        #region Methods
        internal static async Task<App> Create(IQueryExecutor executor, long organisationId, string name)
        {
            //validation - confirm app does not already exists
            if (string.IsNullOrWhiteSpace(name))
                throw new ThisAppException(StatusCodes.Status417ExpectationFailed, Messages.Err417MissingObjectData("App"));
            var existingValue = await executor.Execute(new GetAppQuery(organisationId, name));
            if (existingValue != null)
                throw new ThisAppException(StatusCodes.Status409Conflict, Messages.Err409ObjectExists("App"));
            //create new record
            var value = new App(organisationId, name);
            await executor.Add<App>(value);
            await executor.Save();
            //return record
            return value;
        }
        private App(long organisationId, string name) : base(organisationId, name)
        {
        }
        #endregion //methods
        #region Configuration
        internal class Mapping : IEntityTypeConfiguration<App>
        {
            public void Configure(EntityTypeBuilder<App> builder)
            {
                builder.ToTable("App", "DevOps");
                builder.HasKey(u => u.Id);  // PK.
                builder.Property(p => p.Id).HasColumnName("Id");
                builder.Property(p => p.Name).HasMaxLength(1024).IsRequired();
                // TODO: Setup relationship objects
            }
        }
        #endregion //config
    }
}
