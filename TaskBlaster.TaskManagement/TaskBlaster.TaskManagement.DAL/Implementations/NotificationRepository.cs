using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Entities;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskManagementDbContext _taskManagementDbContext;

        public NotificationRepository(TaskManagementDbContext taskManagementDbContext)
        {
            _taskManagementDbContext = taskManagementDbContext;
        }

        public async Task CreateNewTaskNotificationAsync(int taskId)
        {
            await _taskManagementDbContext.TaskNotifications.AddAsync(new TaskNotification
            {
                TaskId = taskId,
                DueDateNotificationSent = false,
                DayAfterNotificationSent = false,
                LastNotificationDate = null
            });
            await  _taskManagementDbContext.SaveChangesAsync();
        } 
    }
}