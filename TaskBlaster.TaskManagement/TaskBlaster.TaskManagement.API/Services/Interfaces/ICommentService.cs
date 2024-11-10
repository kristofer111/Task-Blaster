using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId);
    Task<bool> AddCommentToTaskAsync(int taskId, CommentInputModel comment);
    Task<bool> RemoveCommentFromTaskAsync(int taskId, int commentId);
}