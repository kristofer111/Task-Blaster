using TaskBlaster.TaskManagement.Notifications.Models;

namespace TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

public interface IRemindersService
{
    Task ProcessTaskNotifications();
    Task<IEnumerable<TaskWithNotificationDto>> SendDueDateReminders();
    Task UpdateTaskNotifications();
}