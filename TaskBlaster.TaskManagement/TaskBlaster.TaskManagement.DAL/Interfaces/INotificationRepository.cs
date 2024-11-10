namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface INotificationRepository
{
    Task CreateNewTaskNotificationAsync(int taskId);
}