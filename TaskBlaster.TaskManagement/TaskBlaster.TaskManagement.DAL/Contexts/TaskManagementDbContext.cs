using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Entities;

namespace TaskBlaster.TaskManagement.DAL.Contexts
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskNotification> TaskNotifications { get; set; }
        public DbSet<User> Users { get; set; }
    }
}