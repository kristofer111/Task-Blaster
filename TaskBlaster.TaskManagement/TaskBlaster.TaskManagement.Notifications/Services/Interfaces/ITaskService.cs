
using TaskBlaster.TaskManagement.Notifications.Models;

namespace TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications();
    
    Task UpdateTaskNotifications();
}