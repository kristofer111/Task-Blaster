using System.ComponentModel.DataAnnotations;

namespace TaskBlaster.TaskManagement.DAL.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}