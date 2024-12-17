using Hangfire;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class HangfireJobsConfigurator
    {
        public static void ConfigureRecurringJobs()
        {
            RecurringJob.AddOrUpdate<RemidersService>(
                "SendDueDateReminders",
                remindersService => remindersService.ProcessTaskNotifications(),
                "*/1 * * * *"
            );
        }
    }
}