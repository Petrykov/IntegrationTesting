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
    public class QuestionsController : IntegrationTest
    {
        [Fact]
        public async Task Get_All_Questions()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/questions");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Question_By_Id()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/questions/61643a9d371ce2924b289979");

            // Assert
            Question question = await response.Content.ReadAsAsync<Question>();
            question.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_New_Question()
        {
            // Arrange
            await AuthenticateAsync();

            var myContent = JsonConvert.SerializeObject(new Question("testQuestion", new string[] { "testAnswer1", "testAnswer2", "testAnswer3" }, 3));
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await TestClient.PostAsync("/api/questions", byteContent);

            // Assert
            Question question = await response.Content.ReadAsAsync<Question>();
            question.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
