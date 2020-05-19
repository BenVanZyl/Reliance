using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Reliance.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reliance.Web.Infrastructure;
using System.Reflection;
using SnowStorm.Infrastructure.Domain;

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
            services.AddRazorPages();

            services.AddMvcCore()
                .AddApiExplorer();

            //AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings")); // TODO: Move Azure Vault

            // Auth
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthDb"))); // TODO: Move Azure Vault
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AuthDbContext>();


            //SnowStorm Config - begin
            
            // Setup query executor & mediatr using SnowStorm package
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration["connectionStrings:DataDb"])); // TODO: Move Azure Vault

            //Configure SnowStorm
            SnowStorm.Infrastructure.Configurations.Setup.All(ref services, typeof(Startup).GetTypeInfo().Assembly, new MappingProfile());

            //SnowStorm Config - end

            SwaggerSetup.Swagger(ref services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MySyncFusion.SetLicence();

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            SwaggerSetup.Swagger(ref app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            
        }
    }
}
