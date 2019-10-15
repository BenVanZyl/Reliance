using Swashbuckle.AspNetCore.Swagger;

namespace Reliance.Web.Infrastructure
{
    public static class ConfigurationSetup
    {
        public static Info SwaggerInfo()
        {
            return new Info()
            {
                Version = "v1",
                Title = "Reliance",
                Description = "Reliance - What is your App relying on?",
                TermsOfService = "Private service. No Unauthorised access allowed.",
                Contact = new Contact()
                {
                    Name = "Ben VZ",
                    Email = "ben.vz@outlook.com", //TODO: setup email address
                    Url = "relaince.githib.com" //TODO: setup github page
                }
            };
        }
    }
}
