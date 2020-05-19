﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reliance.Web.Infrastructure
{
    public static class MySyncFusion
    {
        public static void SetLicence()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LicenseKey);
        }

        public static string LicenseKey => "MTg2OTU4QDMxMzcyZTM0MmUzME52TktQL2R1Y0s2MU5YYzdwcGxmbi9EZGM0NUYxSnpqeTNDNjBGU0wyRUE9;MTg2OTU5QDMxMzcyZTM0MmUzMGVNQ3l1Tld1ZjdWOUowOHpvN3A4OXlYNS9vN05VME8vOXNBWmhQbFRWNHc9;MTg2OTYwQDMxMzcyZTM0MmUzMFVMSmhBdTJ2M210SDArNTBqaG5GUWdwSEtwMUNDMmNML0xSNkRKanB6VWM9;MTg2OTYxQDMxMzcyZTM0MmUzMFpMVTJOSEtLMkFTNUtZSGo2ZEx4VHhvVVJxYkRDRk5wWFU0YlR6VUt0aTg9;MTg2OTYyQDMxMzcyZTM0MmUzMFRndGRYYzRLNklDRm92S0xvK1NNalk5eWQ2UlVMTjM1cEpxeVJ2UWRFUlU9;MTg2OTYzQDMxMzcyZTM0MmUzMGpKVUFoQ1V3enF1SWlGSnlEMkNMaWp4VzVoUUNlOVdTb09Ba3czYVl2V1U9;MTg2OTY0QDMxMzcyZTM0MmUzMENKaEJlUVhUM1gvVWR2UXgvOE55LzhtNmc0U1pTcXRlWDVNRzJxTVhxalE9;MTg2OTY1QDMxMzcyZTM0MmUzMFJ2UDl2NEVsNTNXaHdhWDk0REpTYUZjU2svYWdIT01lbDh6MDlTT04xcFE9;MTg2OTY2QDMxMzcyZTM0MmUzMFhpWkpXME1nR0pNcVZRcFEzQjJCQmxWNjVPVG5TTEV2WjRoa1l3VUN2Zkk9;NT8mJyc2IWhiZH1nfWN9YGpoYmF8YGJ8ampqanNiYmlmamlmanMDHmgxNj0lKRM8Jic/PDw4fTA8Pg==;MTg2OTY3QDMxMzcyZTM0MmUzMG9YY2dyREhhV3RjUXBlOFo0eVdkRHJGV0RTdGsyL3JhWVArUFZnUElzbWM9";

        public static string Version => "17.4.50";

        public static string CcsUrl => $"https://cdn.syncfusion.com/ej2/{Version}/bootstrap4.css";

        public static string JsUrl => $"https://cdn.syncfusion.com/ej2/{Version}/dist/ej2.min.js";

    }
}
