using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reliance.Core.Infrastructure;
using System.Reflection;

namespace Reliance.Web.Test.Infrastructure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //setting up controllers
            services.AddControllers().AddApplicationPart(Assembly.Load("Reliance.Web")).AddControllersAsServices();

            //create temp db and fireup dbUp scripts
            var connectionString = Configuration.GetConnectionString("DataConnection");

            //dbContext
            //AppDbContext.DomainAssemblyName = "Reliance.Core";
            //services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            //services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));

            //wireup query executor & mediatr
            SnowStorm.Setup.All
                (
                    ref services,
                    typeof(Startup).GetTypeInfo().Assembly,
                    new MappingProfile(),
                    connectionString,
                    "Reliance.Core"
                );
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

    }
}
