namespace TaskBlaster.TaskManagement.Models.Dtos;

public class StatusDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}