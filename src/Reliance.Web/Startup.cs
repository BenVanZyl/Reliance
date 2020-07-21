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
using Reliance.Web.Data;
using Reliance.Web.ThisApp.Infrastructure;
using Reliance.Web.ThisApp.Services.Support;
using SnowStorm.Infrastructure.Domain;
using System.Reflection;


namespace Reliance.Web
{
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

            // Setup query executor & mediatr using SnowStorm package
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // TODO: Move Azure Vault

            //configure all of snowstorm -- query executor, mediator, etc.
            SnowStorm.Infrastructure.Configurations.Setup.All(ref services, typeof(Startup).GetTypeInfo().Assembly, new MappingProfile());

            //swagger
            SetupSwagger.AddService(ref services);

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

            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // TODO: Move Azure Vault
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AuthDbContext>();

            //email TODO: Setup something basic and secure...Extend BvzCustom with credentials etc...
            services.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}
