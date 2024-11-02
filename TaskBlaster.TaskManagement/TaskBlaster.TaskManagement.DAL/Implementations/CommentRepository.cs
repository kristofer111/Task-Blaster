using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class CommentRepository : ICommentRepository
{
    public async Task AddCommentToTaskAsync(int taskId, string user, CommentInputModel comment)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCommentFromTaskAsync(int taskId, int commentId)
    {
        throw new NotImplementedException();
    }
}