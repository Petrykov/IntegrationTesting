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
    public class DevelopersControllerTest : IntegrationTest
    {
        [Fact]
        public async Task Get_All_Developers()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/developers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Developer_By_Id()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/developers/6175b3f03e043daa9b7d6113");

            // Assert
            Developer developer = await response.Content.ReadAsAsync<Developer>();
            developer.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_New_Developer()
        {
            // Arrange
            await AuthenticateAsync();

            var myContent = JsonConvert.SerializeObject(new Developer("testEmail", "testPassword", "testName", "testImgSource", new string[] { "1", "2", "3" }, "testPhoneNumber", "testCity", "testDescription", "testOccupationField"));
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await TestClient.PostAsync("/api/developers", byteContent);

            // Assert
            Developer developer = await response.Content.ReadAsAsync<Developer>();
            developer.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
