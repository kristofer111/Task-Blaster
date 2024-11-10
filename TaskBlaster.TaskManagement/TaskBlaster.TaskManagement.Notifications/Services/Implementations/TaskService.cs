using System.Net.Http.Headers;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;
    private readonly IM2MAuthenticationService _m2MAuthenticationService;
    private readonly IConfiguration _configuration;
    private readonly ServiceUriOptions _serviceUriOptions;

    public TaskService(HttpClient httpClient, IM2MAuthenticationService m2MAuthenticationService, IConfiguration configuration, ServiceUriOptions serviceUriOptions)
    {
        _httpClient = httpClient;
        _m2MAuthenticationService = m2MAuthenticationService;
        _configuration = configuration;
        _serviceUriOptions = serviceUriOptions;

        _httpClient.BaseAddress = new Uri($"{serviceUriOptions.TaskManagement}/tasks/notifications/tasks");
    }

    public async Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        var audience = _configuration.GetValue<string>("Auth0:Audience") ?? throw new ArgumentNullException("Auth0:Audience");
        var token = await _m2MAuthenticationService.RetrieveAccessToken(audience);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{_serviceUriOptions.TaskManagement}/tasks/notifications/tasks");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response content: {responseContent}");

        var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<TaskWithNotificationDto>>();
        Console.WriteLine($"Tasks: {tasks}");

        return tasks;
    }

    public async Task UpdateTaskNotifications()
    {   
        var audience = _configuration.GetValue<string>("Auth0:Audience") ?? throw new ArgumentNullException("Auth0:Audience");
        var token = await _m2MAuthenticationService.RetrieveAccessToken(audience);
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var request = new HttpRequestMessage(HttpMethod.Patch, "");
        var response = await _httpClient.SendAsync(request);
        
        response.EnsureSuccessStatusCode();
    }
}