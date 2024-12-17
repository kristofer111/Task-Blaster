using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations;

public class MailjetService : IMailService
{
    private readonly HttpClient _httpClient;
    private readonly ITaskService _taskService;

    private readonly string _apiKey;
    private readonly string _secretKey;

    public MailjetService(HttpClient httpClient, ITaskService taskService, ServiceUriOptions serviceIpOptions, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _taskService = taskService;
        _apiKey = configuration.GetValue<string>("Mailjet:ApiKey") ?? throw new ArgumentNullException("Mailjet:ApiKey");
        _secretKey = configuration.GetValue<string>("Mailjet:SecretKey") ?? throw new ArgumentNullException("Mailjet:SecretKey");

        _httpClient.BaseAddress = new Uri($"{serviceIpOptions.Mailjet}/send");
    }

    // Sends a basic email either using the Mailjet C#
    // wrapper or the Mailjest REST API. A basic email is an email which is not
    // stored as a template within Mailjet

    public async Task<bool> SendBasicEmailAsync(BasicEmailInputModel basicEmail, EmailContentType contentType)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "");

        var emailContent = new
        {
            Messages = new[]
            {
                new
                {
                    From = new { Email = "kristoferorri1@gmail.com", Name = "Task Blaster"},
                    To = new[]
                    {
                        new { Email = basicEmail.To }
                    },
                    basicEmail.Subject,
                    TextPart = basicEmail.Content
                }
            }
        };

        var jsonContent = JsonConvert.SerializeObject(emailContent);
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_apiKey}:{_secretKey}"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return true;
    }

    public Task SendTemplateEmailAsync(string to, string subject, int templateId, Dictionary<string, object> variables)
    {
        throw new NotImplementedException();
    }
}