using System.ComponentModel.DataAnnotations;

namespace TaskBlaster.TaskManagement.Models.Dtos;

public class TaskWithNotificationDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public string? AssignedToUser { get; set; }
    [Required]public TaskNotificationDto Notification { get; set; }
}