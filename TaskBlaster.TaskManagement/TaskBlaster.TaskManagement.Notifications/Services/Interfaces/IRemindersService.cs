namespace TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

public interface IRemindersService
{
    Task ProcessTaskNotifications();
    Task SendDueDateReminders();
    Task UpdateTaskNotifications();
}