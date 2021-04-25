using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Reliance.Web.Services.Support
{
    public interface IApiClient
    {
        public HttpClient Client { get; }

        public WebClient WebClient { get; }
    }

    public class ApiClient: IApiClient
    {
        public ApiClient(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private IIdentity Identity => _httpContextAccessor.HttpContext.User.Identity;

        private WebClient _webClient = null;
        public WebClient WebClient
        {
            get
            {
                if (_webClient == null)
                {
                    _webClient = new WebClient()
                    {
                        UseDefaultCredentials = true,
                        Credentials = Identity as NetworkCredential,
                        BaseAddress = "https://localhost:44376/"
                    };
                }

                return _webClient;
            }
        }

        private HttpClient _client = null;
        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    var handler = new HttpClientHandler
                    {
                        UseDefaultCredentials = true,
                        PreAuthenticate = true,
                        Credentials = Identity as NetworkCredential,

                        //SslProtocols = System.Security.Authentication.SslProtocols.Tls13
                    };
                    _client = new HttpClient(handler)
                    {
                        BaseAddress = new Uri("https://localhost:44376/")
                    };
                    //_client.DefaultRequestHeaders.Add("ContentType", "application/json; charset=utf-8");
                }
                return _client;
            }
        }
    }
}

///
///
//var client = new WebClient()
//{
//    UseDefaultCredentials = true,
//    Credentials = (System.Security.Principal.WindowsIdentity)HttpContext.Current.User.Identity,
//    BaseAddress = "https://localhost:44376/"
//};
//  var response = await client.DownloadStringTaskAsync("api/oranisations");
//  var credentials = new NetworkCredential(User.Identity.Name, User.Identity);
//  var credentials = User.Identity;
