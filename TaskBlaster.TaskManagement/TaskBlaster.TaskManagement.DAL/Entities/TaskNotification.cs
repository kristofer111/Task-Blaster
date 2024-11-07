using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBlaster.TaskManagement.DAL.Entities;

public class TaskNotification
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Task")] public int TaskId { get; set; }
    public Task Task { get; set; } = null!;
    [Required]
    public bool DueDateNotificationSent { get; set; }
    [Required]
    public bool DayAfterNotificationSent { get; set; }
    public DateTime? LastNotificationDate { get; set; }
}
