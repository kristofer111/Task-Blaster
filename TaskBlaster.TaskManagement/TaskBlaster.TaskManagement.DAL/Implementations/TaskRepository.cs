using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;
using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;
    private readonly IClaimsUtility _claimsUtility;
    public TaskRepository(TaskManagementDbContext taskManagementDbContext, IClaimsUtility claimsUtility)
    {
        _taskManagementDbContext = taskManagementDbContext;
        _claimsUtility = claimsUtility;
    }


    // Archives a task by id. Archiving can mean a few things, and
    // depends on your implementation but normally it means that it
    // should not be removed entirely from the database
    public async Task<bool> ArchiveTaskByIdAsync(int taskId)
    {
        var task = await _taskManagementDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
        {
            return false;
        }

        task.IsArchived = true;
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AssignUserToTaskAsync(int taskId, int userId)
    {
        var task = await _taskManagementDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        var user = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (task == null || user == null)
        {
            return false;
        }

        task.AssignedToId = user.Id;
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UnassignUserFromTaskAsync(int taskId, int userId)
    {
        var task = await _taskManagementDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        var user = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (task == null || user == null)
        {
            return false;
        }

        task.AssignedToId = null;
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    public Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        throw new NotImplementedException();
    }

    public async Task<int?> CreateNewTaskAsync(TaskInputModel task)
    {
        var assignedToUser = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == task.AssignedToUser) ??
            await _taskManagementDbContext.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == task.AssignedToUser) ?? null;

        var emailClaim = _claimsUtility.RetrieveUserEmailClaim();

        var createdByUser = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == emailClaim);

        if (createdByUser == null)
        {
            return null;
        }

        var newTask = new Entities.Task
        {
            Title = task.Title,
            Description = task.Description,
            CreatedAt = DateTime.UtcNow,
            DueDate = task.DueDate,
            PriorityId = task.PriorityId,
            StatusId = task.StatusId,
            AssignedToId = assignedToUser?.Id,
            CreatedById = createdByUser.Id
        };

        await _taskManagementDbContext.Tasks.AddAsync(newTask);
        await _taskManagementDbContext.SaveChangesAsync();

        return newTask.Id;
    }

    // Gets a paginated and filtered list of tasks. The tasks should be
    // filtered using the provided filtering object
    // pageSize
    // pageNumber
    // searchValue
    public async Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        var tasks = await _taskManagementDbContext.Tasks
            .Where(t => t.Title.Contains(query.SearchValue) ||
                t.Description.Contains(query.SearchValue))
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status.Name,
                DueDate = t.DueDate,
                AssignedToUser = t.AssignedTo.FullName
            }).ToListAsync();

        Envelope<TaskDto> envelope = new(query.PageNumber, query.PageSize, tasks);
        return envelope;
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
            AssignedToUser = task.AssignedTo?.FullName,
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

    public async Task<bool> UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
    {
        var task = await _taskManagementDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        var status = await _taskManagementDbContext.Statuses
            .FirstOrDefaultAsync(s => s.Id == inputModel.StatusId);

        if (task == null || status == null)
        {
            return false;
        }

        task.StatusId = inputModel.StatusId;
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    public Task UpdateTaskNotifications()
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}