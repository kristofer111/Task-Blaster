using System.Net.Http.Headers;
using System.Text;
using System.Text.Unicode;
using Gateway.Services.Interfaces;
using Newtonsoft.Json;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IM2MAuthenticationService _m2MAuthenticationService;
    private readonly ServiceUriOptions _serviceUriOptions;
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;

    public NotificationService(HttpClient httpClient, IConfiguration configuration, IM2MAuthenticationService m2MAuthenticationService, ServiceUriOptions serviceUriOptions, IUserRepository userRepository, ITaskRepository taskRepository)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _m2MAuthenticationService = m2MAuthenticationService;
        _serviceUriOptions = serviceUriOptions;
        _userRepository = userRepository;
        _taskRepository = taskRepository;

        _httpClient.BaseAddress = new Uri($"{serviceUriOptions.Notifications}/notifications/emails/basic");
    }


    public async Task SendAssignedNotification(int userId, int taskId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new ArgumentNullException("user");
        var task = await _taskRepository.GetTaskByIdAsync(taskId) ?? throw new ArgumentNullException("task");

        var authority = _configuration.GetValue<string>("Auth0:Authority") ?? throw new ArgumentNullException("Auth0:Authority");
        var token = await _m2MAuthenticationService.RetrieveAccessToken(authority);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new
        {
            To = user.Email,
            Subject = "You have been assigned to a new task",
            IsHtml = false,
            Content = $"Hello, {user.FullName}. You have been assigned to a new task: {task.Title}.\n\nPlease check our amazing task management system for more details."
        };
        
        var jsonContent = JsonConvert.SerializeObject(content);
        var request = new HttpRequestMessage(HttpMethod.Post, "");
        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public async Task SendUnassignedNotification(int userId, int taskId)
    {
       var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new ArgumentNullException("user");
       var task = await _taskRepository.GetTaskByIdAsync(taskId) ?? throw new ArgumentNullException("task");

       var audience = _configuration.GetValue<string>("Auth0:Audience") ?? throw new ArgumentNullException("Auth0:Audience");
       var token = await _m2MAuthenticationService.RetrieveAccessToken(audience);

       _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new
        {
            To = user.Email,
            Subject = "Unassigned from task",
            isHtml = false,
            Content = $"You have been assigned from a task: {task.Title}."
        };

       var jsonContent = JsonConvert.SerializeObject(content);
       var request = new HttpRequestMessage(HttpMethod.Post, "");
       request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

       var response = await _httpClient.SendAsync(request);
       response.EnsureSuccessStatusCode();
    }
}