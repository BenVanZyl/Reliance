using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Infrastructure
{
    public static class MySyncFusion
    {
        public static void SetLicence()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LicenseKey);
        }
        public static string LicenseKey => PrivateSettings.SyncFusionLicenseKey;
        public static string Version => PrivateSettings.SyncFusionVersion;
        public static string CcsUrl => $"https://cdn.syncfusion.com/ej2/{Version}/bootstrap4.css";
        public static string JsUrl => $"https://cdn.syncfusion.com/ej2/{Version}/dist/ej2.min.js";
    }
}
