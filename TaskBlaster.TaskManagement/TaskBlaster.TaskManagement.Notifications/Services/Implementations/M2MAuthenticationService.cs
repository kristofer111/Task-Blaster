using System.Text.Json;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class M2MAuthenticationService : IM2MAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private const string Authority = "https://dev-c16iorpujzhnai1o.us.auth0.com/";
        private readonly string _authority;
        private readonly string _clientId;
        private readonly string _clientSecret;

        // In ASP.NET Core, IConfiguration is automatically registered and available for dependency 
        // injection. You don't need to explicitly register it in your Program.cs file. The 
        // WebApplication.CreateBuilder(args) method sets up the configuration for you.
        public M2MAuthenticationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _authority = configuration.GetValue<string>("Auth0:Authority") ?? throw new ArgumentNullException("Auth0:Authority");
            _clientId = configuration.GetValue<string>("Auth0:ClientId") ?? throw new ArgumentNullException("Auth0:ClientId");
            _clientSecret = configuration.GetValue<string>("Auth0:ClientSecret") ?? throw new ArgumentNullException("Auth0:ClientSecret");
            _httpClient.BaseAddress = new Uri($"{_authority}oauth/token");
        }

        public Task<string> RetrieveAccessToken(string audience)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"},
                    {"client_id", _clientId},
                    {"client_secret", _clientSecret},
                    {"audience", audience}
                })
            };

            var response = _httpClient.SendAsync(request).Result;
            var jsonPart = response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(jsonPart.Result);
            return Task.FromResult(authResponse?.access_token ?? "null");
        }
    }
}