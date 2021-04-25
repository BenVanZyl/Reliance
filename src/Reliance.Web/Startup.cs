using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Reliance.Core.Infrastructure;
using Reliance.Web.Data;
using Reliance.Web.Services.Infrastructure;
using Reliance.Web.Services.Support;
using System.Reflection;

namespace Reliance.Web
{
    /// <summary>
    /// Class to be retired!  Was used to experiment and get things working.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auth
            ConfigureAuth(ref services);

            //caching
            services.AddMemoryCache();

            // Razor
            services.AddRazorPages();
            services.AddMvcCore().AddApiExplorer(); //for api controllers
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }); //for api controller & SyncFusion


            // Use Azure SQL Db Integrated security
            //services.AddSingleton<AzureServiceTokenProvider>(new AzureServiceTokenProvider());

            // Moved to SnowStorm
            // Setup query executor & mediatr using SnowStorm package
            //AppDbContext.DomainAssemblyName = "Reliance.Core";
            //services.AddDbContext<AppDbContext>(o => o.UseSqlServer(ThisAppSettings.DataConnectionString)); // TODO: Move Azure Vault

            // Moved to SnowStorm --
            // TODO: need testing to confirm what works
            //services.AddDbContext<AppDbContext>
            //(o => {
            //    var cnnString = Configuration.GetConnectionString("DefaultConnection");
            //    if (cnnString.Contains(".database.windows.net", System.StringComparison.OrdinalIgnoreCase) && !cnnString.Contains("password", System.StringComparison.OrdinalIgnoreCase))
            //    {   // SQL Server Db in Azure, using Azure AD integrated auth
            //        SqlConnection connection = new SqlConnection();
            //        connection.ConnectionString = cnnString;
            //        connection.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
            //        o.UseSqlServer(connection);
            //    }
            //    else
            //        o.UseSqlServer(cnnString); //identity provided in string, not
            //});

            var connectionString = Configuration.GetConnectionString("DataConnection");

            //configure all of snowstorm -- query executor, mediator, automapper, appDbContext
            SnowStorm.Setup.All
                (
                    ref services,
                    typeof(Startup).GetTypeInfo().Assembly,
                    new MappingProfile(),
                    connectionString,
                    "Reliance.Core"
                );

            //swagger
            SetupSwagger.AddService(ref services);

            //setup http client
            services.AddHttpContextAccessor();
            services.AddSingleton<IApiClient, ApiClient>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetupSyncFusion.SetLicence();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //swagger
            SetupSwagger.AddConfiguration(ref app);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        //helpers
        private void ConfigureAuth(ref IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("AuthConnection");
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString)); // TODO: Move Azure Vault
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AuthDbContext>();

            //email TODO: Setup something basic and secure...Extend BvzCustom with credentials etc...
            services.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}
