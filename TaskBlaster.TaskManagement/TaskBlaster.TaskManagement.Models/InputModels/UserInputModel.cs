namespace TaskBlaster.TaskManagement.Models.InputModels;

public class UserInputModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; } = string.Empty;
}