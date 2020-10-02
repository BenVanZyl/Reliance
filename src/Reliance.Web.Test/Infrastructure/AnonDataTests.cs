using RestSharp;
using RestSharp.Authenticators;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reliance.Web.Test.Infrastructure
{
    public class AnonDataTests : BaseForIntegrationTests
    {
        [Fact]
        public async Task AnonDataPingTest()
        {
            // Act
            var response = await Client.GetAsync("/api/anon-data/ping");
            //response.EnsureSuccessStatusCode();
            response.IsSuccessStatusCode.ShouldBeTrue();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            responseString.ShouldBe("Hello World!");

            //
            var client = new RestClient(Server.BaseAddress);//("https://api.twitter.com/1.1");
            client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest("statuses/home_timeline.json", DataFormat.Json);
            var response2 = client.Get(request);
            response2.IsSuccessful.ShouldBeTrue();
            //var responseString2 = await response2.Content.Read;

        }
    }
}
