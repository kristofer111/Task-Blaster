namespace TaskBlaster.TaskManagement.Models.InputModels;

public class TaskCriteriaQueryParams
{
    public string PageSize { get; set; }
    public string PageNumber { get; set; }
    public string? SearchValue { get; set; }
}