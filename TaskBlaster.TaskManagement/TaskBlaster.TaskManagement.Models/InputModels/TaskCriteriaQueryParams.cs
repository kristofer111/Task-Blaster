namespace TaskBlaster.TaskManagement.Models.InputModels;

public class TaskCriteriaQueryParams
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string? SearchValue { get; set; }
}