using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Reliance.Web.Test.Infrastructure
{
    public class BaseForIntegrationTests
    {
        public TestServer Server { get; set; }
        public HttpClient Client { get; set; }

        public BaseForIntegrationTests()
        {
            // Arrange
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                
               )
               ;

            Client = Server.CreateClient();
        }
    }
}
