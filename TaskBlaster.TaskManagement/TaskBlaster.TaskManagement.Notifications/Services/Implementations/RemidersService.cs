using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class RemidersService : IRemindersService
    {
        private readonly ITaskService _taskService;
        private readonly IMailService _mailService;

        public RemidersService(ITaskService taskService, IMailService mailService)
        {
            _taskService = taskService;
            _mailService = mailService;
        }

        public async Task SendDueDateReminders()
        {
            IEnumerable<TaskWithNotificationDto> tasks = await _taskService.GetTasksForNotifications();

            foreach (var task in tasks)
            {
                Console.WriteLine();
                Console.WriteLine($"task: {task}");
                var basicEmail = new BasicEmailInputModel
                {
                    To = task.AssignedToUser ?? "",
                    Subject = "Due date reminder",
                    IsHtml = false,
                    Content = $"This is a friendly reminder that your task '{task.Title}' is due."
                };

                EmailContentType contentType = basicEmail.IsHtml ? EmailContentType.Html : EmailContentType.Text;

                var success = await _mailService.SendBasicEmailAsync(basicEmail, contentType);

                if (!success)
                {
                    Console.WriteLine("Error sending reminder.");
                }
            }
        }
    }
}