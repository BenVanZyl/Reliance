using Newtonsoft.Json;
using Reliance.Web.Client.Dto.Organisations;
using Reliance.Web.Test.Infrastructure;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reliance.Web.Test.IntegrationTests
{
    public class OrganisationTests: BaseForIntegrationTests
    {
        public OrganisationTests(TestServerFixture fixture) : base(fixture)
        {
        }

        // 1. create an organisation
        // 2. get organisation based on id
        // 3. get organisation based on email
        // 4. update organisation based
        // 5. get organisation based on id and confirm update

        public OrganisationDto Organisation { get; set; } //master record for comparision checks

        [Fact]
        public async Task OrganisationReadWrite()
        {

            await CreateOrganisation();

            await GetOrganisationForId();

            await UpdateOrganisationWithId();
        }

        private async Task CreateOrganisation()
        {
            //Arrange
            var org = new OrganisationDto()
            {
                MasterEmail = "tester@integration.testing.host",
                Name = "Integration Tester"
            };
            var data = new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            //Act
            var response = await Client.PostAsync("/api/oranisations", data);

            //Assert
            response.IsSuccessStatusCode.ShouldBeTrue();
            string apiResponse = await response.Content.ReadAsStringAsync();
            Organisation = JsonConvert.DeserializeObject<OrganisationDto>(apiResponse);
            Organisation.Id.ShouldBeGreaterThan(0);
            Organisation.MasterEmail.ShouldBe(org.MasterEmail);
            Organisation.Name.ShouldBe(org.Name);

        }

        private async Task GetOrganisationForId()
        {
            //Arrange
            Organisation.ShouldNotBeNull();
            var id = Organisation.Id;

            //Act
            var response = await Client.GetAsync($"/api/oranisations/{id}");

            //Assert
            response.IsSuccessStatusCode.ShouldBeTrue();
            string apiResponse = await response.Content.ReadAsStringAsync();
            var org = JsonConvert.DeserializeObject<OrganisationDto>(apiResponse);
            org.Id.ShouldBe(Organisation.Id);
            org.MasterEmail.ShouldBe(Organisation.MasterEmail);
            org.Name.ShouldBe(Organisation.Name);
        }

        private async Task UpdateOrganisationWithId()
        {
            //Arrange
            var id = Organisation.Id;
            Organisation.Name = "Updated Integration Tester";
            var data = new StringContent(JsonConvert.SerializeObject(Organisation), Encoding.UTF8, "application/json");

            //Act
            var response = await Client.PutAsync($"/api/oranisations/{id}", data);

            //Assert
            response.IsSuccessStatusCode.ShouldBeTrue();
            string apiResponse = await response.Content.ReadAsStringAsync();
            var org = JsonConvert.DeserializeObject<OrganisationDto>(apiResponse);
            org.Id.ShouldBe(Organisation.Id);
            org.MasterEmail.ShouldBe(Organisation.MasterEmail);
            org.Name.ShouldBe(Organisation.Name);
        }
    }
}
