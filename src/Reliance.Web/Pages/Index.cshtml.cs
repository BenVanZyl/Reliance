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

        public async Task OnPostProfileForm()
        {
            //var captchaResponse = Request.Form["g-recaptcha-response"];
            var reCaptchaSecret = "6LcpqM4ZAAAAAAnEyFupS_c5Ts1qtDpznQcMx62r";
            var reCaptchResponse = Request.Form["g-recaptcha-response"];

            HttpClient httpClient = new HttpClient();
            //var httpResponse = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={reCaptchResponse}").Result;
            //if (httpResponse.StatusCode != HttpStatusCode.OK)
            //{
            //    return;// errorResult.Value;
            //}

            //String jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
            //dynamic jsonData = JObject.Parse(jsonResponse);
            //if (jsonData.success != true.ToString().ToLower())
            //{
            //    return;// errorResult.Value;
            //}

            var content = new CaptchaCheckData()
            {
                secret = reCaptchaSecret,
                response = reCaptchResponse
            };

            var contentString = JsonConvert.SerializeObject(content);

            var response = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", new StringContent(contentString, Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return;// errorResult.Value;
            }

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            dynamic data = JObject.Parse(jsonResponse);
            if (data.success != true.ToString().ToLower())
            {
                return;// errorResult.Value;
            }

            //var result = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify");

        }

        private class CaptchaCheckData
        {
            public string secret { get; set; }
            public string response { get; set; }
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