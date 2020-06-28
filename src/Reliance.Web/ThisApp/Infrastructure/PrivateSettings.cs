using Microsoft.Extensions.Configuration;
using System.IO;

namespace Reliance.Web.ThisApp.Infrastructure
{
    public class PrivateSettings
    {
        public static string SyncFusionLicenseKey => Configuration["AppSettings:SyncFusionLicenseKey"];
        public static string SyncFusionVersion => Configuration["AppSettings:SyncFusionVersion"];
        public static string EmailServerAddress => Configuration["AppSettings:EmailServerAddress"];
        public static int EmailServerPort => int.TryParse(Configuration["AppSettings:EmailServerPort"], out int v) ? v : 0;
        public static bool EmailEnableSsl => bool.TryParse(Configuration["AppSettings:EmailEnableSsl"], out bool v) ? v : false;
        public static string EmailUserName => Configuration["AppSettings:EmailUserName"];
        public static string EmailPassword => Configuration["AppSettings:EmailPassword"];

        // Configuration setup
        private static IConfigurationRoot _configuration = null;
        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("privatesettings.json", optional: true, reloadOnChange: true);

                    _configuration = builder.Build();
                }
                return _configuration;
            }
            set { _configuration = value; }
        }
    }
}

/*
privatesettings.json

{
  "AppSettings": {
    "SyncFusionLicenseKey": "",
    "SyncFusionVersion": "",
    "EmailServerAddress": "",
    "EmailServerPort": 587,
    "EmailEnableSsl": false,
    "EmailUserName": "",
    "EmailPassword": ""
  }
}


file must be marked as content.
 */
