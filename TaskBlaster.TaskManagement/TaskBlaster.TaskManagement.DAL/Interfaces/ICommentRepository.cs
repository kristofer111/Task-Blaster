using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId);

    Task<bool> AddCommentToTaskAsync(int taskId, string username, CommentInputModel comment);

    Task<bool> RemoveCommentFromTaskAsync(int taskId, int commentId);
}