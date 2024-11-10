using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IClaimsService _claimsService;

    public CommentService(ICommentRepository commentRepository, IClaimsService claimsService)
    {
        _commentRepository = commentRepository;
        _claimsService = claimsService;
    }


    public async Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId)
    {
        return await _commentRepository.GetCommentsAssociatedWithTaskAsync(taskId);
    }

    public async Task<bool> AddCommentToTaskAsync(int taskId, CommentInputModel comment)
    {
        string username = _claimsService.RetrieveUserNameClaim();
        return await _commentRepository.AddCommentToTaskAsync(taskId, username, comment);
    }

    public async Task<bool> RemoveCommentFromTaskAsync(int taskId, int commentId)
    {
        return await _commentRepository.RemoveCommentFromTaskAsync(taskId, commentId);
    }
}