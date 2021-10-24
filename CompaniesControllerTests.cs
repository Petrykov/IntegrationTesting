using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend_dockerAPI.Models;
using FluentAssertions;
using IntegrationTests;
using Newtonsoft.Json;
using Xunit;

namespace backendIntegrationTests
{
    public class CompaniesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_All_Companies()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/companies");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Company_By_Id()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/companies/6175bfd83b4e3eda3b63851b");

            // Assert
            Company company = await response.Content.ReadAsAsync<Company>();
            company.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_New_Company()
        {
            // Arrange
            await AuthenticateAsync();

            var myContent = JsonConvert.SerializeObject(new Company("testEmail", "testPassword", "testName", "testImgSource", new string[] { "1", "2", "3" }));
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await TestClient.PostAsync("/api/companies", byteContent);

            // Assert
            Company company = await response.Content.ReadAsAsync<Company>();
            company.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
