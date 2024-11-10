using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Controllers;

// [Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ICommentService _commentService;
    private readonly string _audience;

    public TasksController(ITaskService taskService, ICommentService commentService, IConfiguration configuration)
    {
        _taskService = taskService;
        _commentService = commentService;
        _audience = configuration.GetValue<string>("Auth0:Audience") ?? throw new ArgumentNullException("Auth0:Audience");
    }


    [HttpGet("")]
    public async Task<ActionResult<Envelope<TaskDto>>> GetPaginatedTasksByCriteria([FromQuery] TaskCriteriaQueryParams query)
    {
        var tasks = await _taskService.GetPaginatedTasksByCriteriaAsync(query);
        return Ok(tasks);
    }

    [HttpGet("{taskId}", Name = "GetTaskById")]
    public async Task<ActionResult<TaskDetailsDto?>> GetTaskById(int taskId)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);

        if (task == null)
        {
            return NotFound(new { message = $"Task with id {taskId} was not found" });
        }

        return Ok(task);
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateNewTask([FromBody] TaskInputModel task)
    {
        var newId = await _taskService.CreateNewTaskAsync(task);

        if (newId == null)
        {
            return BadRequest(new { message = "Task could not be created because the token is missing or does not have required claims" });
        }

        return CreatedAtRoute("GetTaskById", new { taskId = newId }, null);
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> ArchiveTaskById(int taskId)
    {
        var success = await _taskService.ArchiveTaskByIdAsync(taskId);

        if (!success)
        {
            return NotFound(new { message = $"Task with id {taskId} was not found" });
        }

        return Ok();
    }

    [HttpPatch("{taskId}/assign/{userId}")]
    public async Task<ActionResult> AssignUserToTask(int taskId, int userId)
    {
        var success = await _taskService.AssignUserToTaskAsync(taskId, userId);

        if (!success)
        {
            return NotFound(new { message = $"Either task or user was not found. Or The task has already been assigned to a user." });
        }

        return Ok();
    }

    [HttpPatch("{taskId}/unassign/{userId}")]
    public async Task<ActionResult> UnassignUserFromTask(int taskId, int userId)
    {
        var success = await _taskService.UnassignUserFromTaskAsync(taskId, userId);

        if (!success)
        {
            return NotFound(new { message = $"Either task or user was not found" });
        }

        return Ok();
    }

    [HttpPatch("{taskId}/status")]
    public async Task<ActionResult> UpdateTaskStatus(int taskId, [FromBody] StatusInputModel inputModel)
    {
        var success = await _taskService.UpdateTaskStatusAsync(taskId, inputModel);

        if (!success)
        {
            return NotFound(new { message = $"Either task or status was not found" });
        }

        return Ok();
    }

    [HttpPatch("{taskId}/priority")]
    public async Task<ActionResult> UpdateTaskPriority(int taskId, [FromBody] PriorityInputModel inputModel)
    {
        var success = await _taskService.UpdateTaskPriorityAsync(taskId, inputModel);

        if (!success)
        {
            return NotFound(new { message = $"Either task or priority was not found" });
        }

        return Ok();
    }

    [HttpGet("{taskId}/comments")]
    public async Task<ActionResult> GetCommentsAssociatedWithTask(int taskId)
    {
        var comments = await _commentService.GetCommentsAssociatedWithTaskAsync(taskId);
        return Ok(comments);
    }

    // TODO add author to comment
    [HttpPost("{taskId}/comments")]
    public async Task<ActionResult> AddCommentToTask(int taskId, [FromBody] CommentInputModel inputModel)
    {
        var success = await _commentService.AddCommentToTaskAsync(taskId, inputModel);

        if (!success)
        {
            return NotFound(new { message = $"Task with id {taskId} was not found" });
        }

        return Ok();
    }

    [HttpDelete("{taskId}/comments/{commentId}")]
    public async Task<ActionResult> RemoveCommentFromTask(int taskId, int commentId)
    {
        var success = await _commentService.RemoveCommentFromTaskAsync(taskId, commentId);

        if (!success)
        {
            return NotFound(new { message = $"Comment with id {commentId} was not found" });
        }

        return Ok();
    }

    [HttpGet("notifications/tasks")]
    public async Task<ActionResult<IEnumerable<TaskWithNotificationDto>>> GetTasksForNotifications()
    {
        var tasks = await _taskService.GetTasksForNotifications();
        return Ok(tasks);
    }

    [HttpPatch("notifications/tasks")]
    public async Task<ActionResult> UpdateTaskNotifications()
    {
        await _taskService.UpdateTaskNotifications();
        return Ok();
    }
}
