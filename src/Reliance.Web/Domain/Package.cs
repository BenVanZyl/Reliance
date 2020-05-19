using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reliance.Web.Services.Queries;
using SnowStorm.Infrastructure.Domain;
using SnowStorm.Infrastructure.QueryExecutors;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Reliance.Web.Domain
{
    public class Package : DomainEntityWithIdInt64Audit64
    {
        protected Package() { }
        //public int Id { get; private set; }  // PK is Mapped to DomainEnitiy.Id
        public string Name { get; private set; }
        public string FullVersion { get; private set; }
        public int VersionMaster { get; private set; }
        public int VersionMinor { get; private set; }
        public int VersionPatch { get; private set; }
        public string TargetFrameWork { get; private set; }

        #region Methods

        public static async Task<Package> Create(IQueryExecutor executor, Client.Api.PackageDto data)
        {
            //var x = Microsoft.Extensions.DependencyInjection.Extensions.ServiceCollectionDescriptorExtensions.Equals(typeof(IQueryExecutor));
            //    GetService<IQueryExecutor>();


            Package newPackage = null;
            newPackage = await executor.ExecuteAsync(new GetPackageQuery(data));
            if (newPackage == null)
            {
                newPackage = new Package(data);
                await executor.Add(newPackage);
            }
            return newPackage;
        }

        public Package(Client.Api.PackageDto data)
        {
            Update(data);
        }

        public void Update(Client.Api.PackageDto data)
        {
            Name = data.Name;
            FullVersion = data.FullVersion;
            VersionMaster = data.VersionMaster;
            VersionMinor = data.VersionMinor;
            VersionPatch = data.VersionPatch;
            TargetFrameWork = data.TargetFrameWork;
        }

        #endregion

        internal class EntityMap : IEntityTypeConfiguration<Package>
        {
            public void Configure(EntityTypeBuilder<Package> builder)
            {
                builder.ToTable("Package", "Reliance");
                builder.HasKey(u => u.Id);  // PK. 
                builder.Property(p => p.Id).HasColumnName("Id"); //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                builder.Property(p => p.Name).HasMaxLength(1024);
                builder.Property(p => p.TargetFrameWork).HasMaxLength(255);
            }
        }
    }
}
