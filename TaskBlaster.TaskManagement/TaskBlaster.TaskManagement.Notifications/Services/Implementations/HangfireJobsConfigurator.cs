using Hangfire;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class HangfireJobsConfigurator
    {
        // ég veit að ég á að kalla á SendBasicEmail þegar ég assign-a 
        // eða unassign-a einhvern á task

        // Þú þarft að búa til Hangfire Job (sem er execute-að á 30mins fresti), 
        // þetta job notar TaskService. Það ætti að sækja öll þau tasks sem þarf 
        // að senda út notification og update-a þau.

        // Þannig þarft að búa til nýjan file og gera klassa sem sér um þessa logic
        public static void ConfigureRecurringJobs()
        {
            RecurringJob.AddOrUpdate<TaskService>(
                "GetTasksForNotificationsEvery30Minutes", // Job ID
                taskService => taskService.GetTasksForNotifications(), // Method to call
                "*/1 * * * *" // CRON expression for every 30 minutes
            );
        }
    }
}