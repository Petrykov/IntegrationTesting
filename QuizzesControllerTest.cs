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
    public class QuizzesControllerTest : IntegrationTest
    {
        [Fact]
        public async Task Get_All_Quizzes()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/quizzes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Quiz_By_Id()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/api/quizzes/617f250128318130e3e9f4e4");

            // Assert
            Quiz quiz = await response.Content.ReadAsAsync<Quiz>();
            quiz.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_New_Quiz()
        {
            // Arrange
            await AuthenticateAsync();

            var myContent = JsonConvert.SerializeObject(new Quiz("615b0ab87f4732e8736a1f95", "testName", "testRequiredStack", 5, new string[] { "61643a9d371ce2924b289979", "61659ae0751e1515cc7114a9" }, 3));
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await TestClient.PostAsync("/api/quizzes", byteContent);

            // Assert
            Quiz Quiz = await response.Content.ReadAsAsync<Quiz>();
            Quiz.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
