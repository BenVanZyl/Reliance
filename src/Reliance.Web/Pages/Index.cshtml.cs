using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Reliance.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

    }
}

// https://dejanstojanovic.net/aspnet/2018/may/using-google-recaptcha-v2-in-aspnet-core-web-application/
// Automation testing
// If you have any automation testing in place for your pages, they will probably fail as Google reCAPTCHA will recognize them as automated clicks and there for it will set models as invalid. For this scenario Google has specified set of SecretKey and SiteKey specially for automation testing:
//
// Site key: 6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI
// Secret key: 6LeIxAcTAAAAAGG - vFI1TnRWxMZNFuojJ4WifJWe
// NOTE
// These valuse may chnage over time so check them at https://developers.google.com/recaptcha/docs/faq
//