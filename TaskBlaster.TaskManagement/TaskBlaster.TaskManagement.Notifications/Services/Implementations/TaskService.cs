using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient, IM2MAuthenticationService m2MAuthenticationService, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var token = m2MAuthenticationService.RetrieveAccessToken(configuration.GetValue<string>("IpAddresses:TaskManagementIpAddress"));
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