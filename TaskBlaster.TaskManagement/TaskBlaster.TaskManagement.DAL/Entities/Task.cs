using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBlaster.TaskManagement.DAL.Entities;

public class Task
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    [ForeignKey("Priority")] public int? PriorityId { get; set; }
    public Priority Priority { get; set; } = null!;
    [ForeignKey("Status")] public int? StatusId { get; set; }
    public Status Status { get; set; } = null!;
    [ForeignKey("AssignedTo")] public int? AssignedToId { get; set; }
    public User AssignedTo { get; set; } = null!;
    [ForeignKey("CreatedBy")] public int? CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    public bool IsArchived { get; set; }
}