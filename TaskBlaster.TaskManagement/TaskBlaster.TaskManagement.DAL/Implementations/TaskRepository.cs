using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Http;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using Npgsql.Replication;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;
    private readonly IProfileService _profileService;

    public TaskRepository(TaskManagementDbContext taskManagementDbContext, IProfileService profileService)
    {
        _taskManagementDbContext = taskManagementDbContext;
        _profileService = profileService;
    }

    public Task ArchiveTaskByIdAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task AssignUserToTaskAsync(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> CreateNewTaskAsync(TaskInputModel task, string emailClaim)
    {
        var assignedToUser = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == task.AssignedToUser) ??
            await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == task.AssignedToUser);

        // var emailClaim = _profileService.GetUserEmail();
        Console.WriteLine("EmailClaim:", emailClaim);

        var createdByUser = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == emailClaim);

        if (createdByUser == null)
        {
            return null;
        }

        var createById = createdByUser.Id;

        var newTask = new Entities.Task
        {
            Title = task.Title,
            Description = task.Description,
            CreatedAt = DateTime.UtcNow,
            DueDate = task.DueDate,
            PriorityId = task.PriorityId,
            StatusId = task.StatusId,
            AssignedToId = assignedToUser.Id,
            CreatedById = createById,
        };

        await _taskManagementDbContext.Tasks.AddAsync(newTask);
        await _taskManagementDbContext.SaveChangesAsync();

        return newTask.Id;
    }

    public Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
    {
        var task = await _taskManagementDbContext.Tasks
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
        {
            return null;
        }

        return new TaskDetailsDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status.Name,
            Priority = task.Priority.Name,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            CreatedBy = task.CreatedBy.FullName,
            AssignedToUser = task.AssignedTo.FullName,
            Tags = task.Tags
                .Select(tag => tag.Name)
                .ToList(),
            Comments = _taskManagementDbContext.Comments
                .Where(c => c.TaskId == taskId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Author = c.Author,
                    ContentAsMarkdown = c.ContentAsMarkdown,
                    CreatedAt = c.CreatedDate
                }).ToList()
        };
    }

    public Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        throw new NotImplementedException();
    }

    public Task UnassignUserFromTaskAsync(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskNotifications()
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}