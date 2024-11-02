using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBlaster.TaskManagement.DAL.Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Author { get; set; } = null!;
    [Required]
    public string ContentAsMarkdown { get; set; } = null!;
    [Required]
    public DateTime CreatedDate { get; set; }
    [ForeignKey("Task")]
    public int TaskId { get; set; }
    public Task Task { get; set; } = null!;
}
