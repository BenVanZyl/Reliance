using Reliance.Web.Test.Infrastructure;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reliance.Web.Test.IntegrationTests
{
    public class OrganisationTests: BaseForIntegrationTests
    {

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var response = await Client.GetAsync("/api/oranisations");
            //response.EnsureSuccessStatusCode();
            response.IsSuccessStatusCode.ShouldBeTrue();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello World!", responseString);
        }
    }
}
