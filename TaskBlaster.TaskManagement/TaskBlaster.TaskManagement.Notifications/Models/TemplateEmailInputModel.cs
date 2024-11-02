using Microsoft.Build.Framework;

namespace TaskBlaster.TaskManagement.Notifications.Models;

public class TemplateEmailInputModel
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string TemplateId { get; set; } = string.Empty;
    [Required]public Dictionary<string,object> Variables { get; set; }
}