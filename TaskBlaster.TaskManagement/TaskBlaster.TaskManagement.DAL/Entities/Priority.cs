using System.ComponentModel.DataAnnotations;

namespace TaskBlaster.TaskManagement.DAL.Entities;

public class Priority
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = "";

}