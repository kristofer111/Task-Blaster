using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Entities;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public CommentRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }


    public async Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId)
    {
        var comments = await _taskManagementDbContext.Comments
            .Where(c => c.TaskId == taskId)
            .ToListAsync();

        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Author = c.Author,
            ContentAsMarkdown = c.ContentAsMarkdown,
            CreatedAt = c.CreatedDate
        });
    }

    public async Task<bool> AddCommentToTaskAsync(int taskId, string username, CommentInputModel comment)
    {
        if (!await _taskManagementDbContext.Tasks.AnyAsync(t => t.Id == taskId))
        {
            return false;
        }

        await _taskManagementDbContext.Comments.AddAsync(new Comment
        {
            Author = username,
            ContentAsMarkdown = comment.ContentAsMarkdown,
            CreatedDate = DateTime.UtcNow,
            TaskId = taskId
        });
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveCommentFromTaskAsync(int taskId, int commentId)
    {
        var task = await _taskManagementDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        var comment = await _taskManagementDbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            return false;
        }

        _taskManagementDbContext.Comments.Remove(comment);
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }
}