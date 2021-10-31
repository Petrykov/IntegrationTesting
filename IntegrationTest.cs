using System;
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

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            TestClient.BaseAddress = new Uri("https://backend-server-heroku.herokuapp.com");
            var user = new { Email = "testingAccount", Password = "Vv11111" };
            var response = await TestClient.PostAsJsonAsync("/api/login", user);
            var registrationResponse = await response.Content.ReadAsAsync<AuthenticationResult>();
            return registrationResponse.Token;
        }
    }
}
