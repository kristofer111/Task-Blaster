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

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    /// <summary>
    /// Returns all tasks by a provided criteria as a paginated result
    /// </summary>
    /// <param name="query">A query which is used to paginate and filter the result</param>
    /// <returns>A filtered and paginated list of tasks</returns>
    [HttpGet("")]
    public async Task<ActionResult<Envelope<TaskDto>>> GetPaginatedTasksByCriteria([FromQuery] TaskCriteriaQueryParams query)
    {
        var tasks = await _taskService.GetPaginatedTasksByCriteriaAsync(query);
        return Ok(tasks);
    }

    /// <summary>
    /// Returns a single task by id
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <returns>A single task or null</returns>
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

    /// <summary>
    /// Creates a new task
    /// </summary>
    /// <param name="task">Input model used to populate the new task</param>
    [HttpPost("")]
    public async Task<ActionResult> CreateNewTask([FromBody] TaskInputModel task)
    {
        var newId = await _taskService.CreateNewTaskAsync(task);

        if (newId == null)
        {
            return BadRequest(new { message = "Task could not be created because the token is missing required claims" });
        }

        return CreatedAtRoute("GetTaskById", new { taskId = newId }, null);
    }

    /// <summary>
    /// Archives a task by id
    /// </summary>
    /// <param name="taskId">The id of the task which should be archived</param>
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

    /// <summary>
    /// Assigns a user from a task by id
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="userId">The id of the user which should be assigned</param>
    [HttpPatch("{taskId}/assign/{userId}")]
    public async Task<ActionResult> AssignUserToTask(int taskId, int userId)
    {
        var success = await _taskService.AssignUserToTaskAsync(taskId, userId);

        if (!success)
        {
            return NotFound(new { message = $"Either task or user was not found" });
        }

        return Ok();
    }

    /// <summary>
    /// Unassigns a user from a task by id
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="userId">The id of the user which should be unassigned</param>
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

    /// <summary>
    /// Updates the status of a task, e.g. 'pending', 'completed'
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model associated with the status update</param>
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

    /// <summary>
    /// Updates the priority of a task, e.g. 'Critical', 'High'
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model associated with the priority update</param>
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

    /// <summary>
    /// Gets all comments associated with a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <returns>A list of comments</returns>
    [HttpGet("{taskId}/comments")]
    public async Task<ActionResult> GetCommentsAssociatedWithTask(int taskId)
    {
        var comments = await _taskService.GetCommentsAssociatedWithTaskAsync(taskId);
        return Ok(comments);
    }

    /// <summary>
    /// Adds a single comment to a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model for the comment</param>
    [HttpPost("{taskId}/comments")]
    public async Task<ActionResult> AddCommentToTask(int taskId, [FromBody] CommentInputModel inputModel)
    {
        var success = await _taskService.AddCommentToTaskAsync(taskId, inputModel);

        if (!success)
        {
            return NotFound(new { message = $"Task with id {taskId} was not found" });
        }

        return Ok();
    }

    /// <summary>
    /// Removes a comment from a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="commentId">The id of the comment</param>
    [HttpDelete("{taskId}/comments/{commentId}")]
    public async Task<ActionResult> RemoveCommentFromTask(int taskId, int commentId)
    {
        var success = await _taskService.RemoveCommentFromTaskAsync(taskId, commentId);

        if (!success)
        {
            return NotFound(new { message = $"Comment with id {commentId} was not found" });
        }

        return Ok();
    }

    // test function
    [HttpGet("notifications")]
    public async Task<ActionResult<IEnumerable<TaskWithNotificationDto>>> GetTasksForNotifications()
    {
        var res = await _taskService.GetTasksForNotifications();
        return Ok(res);
    }
}
