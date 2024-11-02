using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class NotificationService : INotificationService
{
    public Task SendAssignedNotification(int userId, int taskId)
    {
        throw new NotImplementedException();
    }

    public Task SendUnassignedNotification(int userId, int taskId)
    {
        throw new NotImplementedException();
    }
}