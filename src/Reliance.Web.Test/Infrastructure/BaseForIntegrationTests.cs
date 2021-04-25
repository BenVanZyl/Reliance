using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Builder;
//using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Reliance.Web.Test.Infrastructure
{
    public class BaseForIntegrationTests: IDisposable, IClassFixture<TestServerFixture>
    {
        public BaseForIntegrationTests(TestServerFixture fixture) => Fixture = fixture;

        public TestServerFixture Fixture { get; }
        public TestServer TestServer => Fixture.TestServer;
        public HttpClient Client => Fixture.Client;

        public void Dispose()
        {
            //Client.Dispose();
            //TestServer.Dispose();
        }
    }
}
