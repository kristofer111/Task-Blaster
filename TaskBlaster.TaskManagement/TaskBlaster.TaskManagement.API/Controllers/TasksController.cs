using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
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
    public Task<ActionResult<Envelope<TaskDto>>> GetPaginatedTasksByCriteria([FromQuery] TaskCriteriaQueryParams query)
    {
        throw new NotImplementedException();
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
            return NoContent();
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
            return Conflict("newId is null");
        }
        return Ok(newId);
    }

    /// <summary>
    /// Archives a task by id
    /// </summary>
    /// <param name="taskId">The id of the task which should be archived</param>
    [HttpDelete("{taskId}")]
    public Task<ActionResult> ArchiveTaskById(int taskId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Assigns a user from a task by id
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="userId">The id of the user which should be assigned</param>
    [HttpPatch("{taskId}/assign/{userId}")]
    public Task<ActionResult> AssignUserToTask(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Unassigns a user from a task by id
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="userId">The id of the user which should be unassigned</param>
    [HttpPatch("{taskId}/unassign/{userId}")]
    public Task<ActionResult> UnassignUserFromTask(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the status of a task, e.g. 'pending', 'completed'
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model associated with the status update</param>
    [HttpPatch("{taskId}/status")]
    public Task<ActionResult> UpdateTaskStatus(int taskId, [FromBody] StatusInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the priority of a task, e.g. 'Critical', 'High'
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model associated with the priority update</param>
    [HttpPatch("{taskId}/priority")]
    public Task<ActionResult> UpdateTaskPriority(int taskId, [FromBody] PriorityInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets all comments associated with a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <returns>A list of comments</returns>
    [HttpGet("{taskId}/comments")]
    public Task<ActionResult> GetCommentsAssociatedWithTask(int taskId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Adds a single comment to a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model for the comment</param>
    [HttpPost("{taskId}/comments")]
    public Task<ActionResult> AddCommentToTask(int taskId, [FromBody] CommentInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes a comment from a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="commentId">The id of the comment</param>
    [HttpDelete("{taskId}/comments/{commentId}")]
    public Task<ActionResult> RemoveCommentFromTask(int taskId, int commentId)
    {
        throw new NotImplementedException();
    }
}
