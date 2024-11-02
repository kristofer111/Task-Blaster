namespace TaskBlaster.TaskManagement.Models.Dtos;

public class TaskDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? AssignedToUser { get; set; }
    public List<string> Tags { get; set; } = [];
    public List<CommentDto> Comments { get; set; } = [];
}