using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend_dockerAPI;
using backend_dockerAPI.Results;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }

        protected async Task Authenticate()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("/api/login", new { Email = "Alex", Password = "password" });
            var registrationResponse = await response.Content.ReadAsAsync<AuthenticationResult>();
            return registrationResponse.Token;
        }
    }
}
