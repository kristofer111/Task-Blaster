using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface ITaskRepository
{
    Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query);

    Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId);

    Task<int?> CreateNewTaskAsync(TaskInputModel task);

    Task<bool> ArchiveTaskByIdAsync(int taskId);

    Task<bool> AssignUserToTaskAsync(int taskId, int userId);

    Task<bool> UnassignUserFromTaskAsync(int taskId, int userId);

    Task<bool> UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel);

    Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications();
    
    Task<bool> UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel);
    
    Task UpdateTaskNotifications();

    Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId);

    Task<bool> AddCommentToTaskAsync(int taskId, CommentInputModel inputModel);

    Task<bool> RemoveCommentFromTaskAsync(int taskId, int commentId);
}