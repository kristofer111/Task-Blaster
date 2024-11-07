using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;
    private readonly ITaskRepository _taskRepository;

    public TaskService(HttpClient httpClient, IM2MAuthenticationService m2MAuthenticationService, IConfiguration configuration, ServiceIpOptions serviceIpOptions, ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        _httpClient = httpClient;
        var token = m2MAuthenticationService.RetrieveAccessToken(configuration.GetValue<string>("Auth0:Audience"));
        _httpClient.BaseAddress = new Uri($"{serviceIpOptions.TaskManagement}/api/orders/");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }


    public Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskNotifications()
    {
        throw new NotImplementedException();
    }
}