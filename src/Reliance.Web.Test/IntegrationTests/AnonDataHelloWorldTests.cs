using Reliance.Web.Test.Infrastructure;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reliance.Web.Test.IntegrationTests
{
    public class AnonDataHelloWorldTests : BaseForIntegrationTests
    {
        public AnonDataHelloWorldTests(TestServerFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ReturnHelloWorldNoHttpsUsingServer()
        {
            // Act
            var response = await TestServer.CreateRequest("/api/anon-data/ping").SendAsync("GET");

            // Assert
            response.IsSuccessStatusCode.ShouldBeFalse();  // Server only allows for https connections
            //response.IsSuccessStatusCode.ShouldBeTrue();
            //var responseString = await response.Content.ReadAsStringAsync();
            //responseString.ShouldBe("Hello World!");
        }


        [Fact]
        public async Task ReturnHelloWorldHttpsUsingClient()
        {
            //Arrange
            //Client.BaseAddress = new Uri(Client.BaseAddress.ToString().Replace("http:", "https:"));

            // Act
            var response = await Client.GetAsync("/api/anon-data/ping/secure");

            // Assert
            response.IsSuccessStatusCode.ShouldBeTrue();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Hello World!", responseString);
            responseString.ShouldBe("Hello World!");

        }
    }
}
