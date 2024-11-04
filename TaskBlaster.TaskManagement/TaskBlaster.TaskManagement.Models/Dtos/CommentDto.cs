using System.ComponentModel.DataAnnotations;

namespace TaskBlaster.TaskManagement.Models.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string ContentAsMarkdown { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}