using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reliance.Web.Data;
using Reliance.Web.Infrastructure;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            // Auth and Identity
            services.AddDbContext<AuthDbContext>(o => o.UseSqlServer(Configuration["connectionStrings:DefaultConnection"]));
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AuthDbContext>();

            // App Db Context
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration["connectionStrings:DefaultConnection"]));

            SnowStorm.Infrastructure.Configurations.Setup.All(ref services, typeof(Startup).GetTypeInfo().Assembly, new MappingProfile(), ConfigurationSetup.SwaggerInfo());
                //services.AddScoped<ICompanyBrandRepository, CompanyBrandRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseCookiePolicy();

            app.UseAuthentication();

            //seting up swagger in the UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reliance - What is your App relying on?");
                c.RoutePrefix = "swagger";
            });

            app.UseMvc();
        }
    }
}
