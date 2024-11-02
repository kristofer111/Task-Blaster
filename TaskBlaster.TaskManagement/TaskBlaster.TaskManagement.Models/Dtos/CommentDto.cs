using System.ComponentModel.DataAnnotations;

namespace TaskBlaster.TaskManagement.Models.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    [Required]public string Author { get; set; } = string.Empty;
    public string ContentAsMarkdown { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}