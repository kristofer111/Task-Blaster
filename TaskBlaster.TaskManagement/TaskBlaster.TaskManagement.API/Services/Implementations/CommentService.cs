using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class CommentService : ICommentService
{
    public Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task AddCommentToTaskAsync(int taskId, string user, CommentInputModel comment)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCommentFromTaskAsync(int taskId, int commentId)
    {
        throw new NotImplementedException();
    }
}