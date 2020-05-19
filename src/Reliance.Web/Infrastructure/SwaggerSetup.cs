using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Reliance.Web.Infrastructure
{
    /// <summary>
    /// Swagger: https://github.com/domaindrivendev/Swashbuckle.AspNetCore
    /// </summary>
    public static class SwaggerSetup
    {
        public static OpenApiInfo SwaggerInfo()
        {
            return new OpenApiInfo()
            {
                Version = "v1",
                Title = "Reliance",
                Description = "Reliance - What is your App relying on?",
                //TermsOfService = "url for license...github", //Private service. No Unauthorised access allowed.
                Contact = new OpenApiContact()
                {
                    Name = "BvZ",
                    Email = "benvz@outlook.com", //TODO: setup email address
                    Url = new System.Uri("https://github.com/BenVanZyl/Reliance") //TODO: setup github page
                }
            };
        }

        public static void Swagger(ref IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", SwaggerInfo());
            });
        }

        public static void Swagger(ref IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reliance API V1");
            });
        }
    }
}
