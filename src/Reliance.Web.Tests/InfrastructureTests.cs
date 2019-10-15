using Shouldly;
using SnowStorm.Infrastructure;
using System.Net.Http;
using Xunit;

namespace Reliance.Web.Tests
{
    public class InfrastructureTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public InfrastructureTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public void VerifyIsRunningAsTest()
        {
            // code is running in unit test mode
            RunningAs.UnitTest.ShouldBeTrue();

            // code is not running in a normal production type mode
            RunningAs.Normal.ShouldBeFalse();
        }
    }
}
