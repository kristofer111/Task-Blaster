using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public TaskRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }

    // add tags to tasks will not be implemented


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

        if (task == null || user == null || task.AssignedToId != null)
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

    public async Task<int?> CreateNewTaskAsync(TaskInputModel task, string emailClaim)
    {
        var createdByUser = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == emailClaim);

        if (createdByUser == null)
        {
            return null;
        }

        var assignedToUser = await _taskManagementDbContext.Users
            .FirstOrDefaultAsync(u => u.EmailAddress == task.AssignedToUser) ??
            await _taskManagementDbContext.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == task.AssignedToUser) ?? null;

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
        var tasks = query.SearchValue != null ?
        _taskManagementDbContext.Tasks
            .Where(t => t.Title.Contains(query.SearchValue) ||
                t.Description.Contains(query.SearchValue)) :
        _taskManagementDbContext.Tasks;

        var tasksDtos = await tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status.Name,
            DueDate = t.DueDate,
            AssignedToUser = t.AssignedTo.FullName
        }).ToListAsync();

        Envelope<TaskDto> envelope = new(query.PageNumber, query.PageSize, tasksDtos);
        return envelope;
    }

    public async Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
    {
        Console.WriteLine($"DateTime.UtcNow: {DateTime.UtcNow}");
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
            CreatedBy = task.CreatedBy?.FullName ?? "",
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

        task.StatusId = status.Id;
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        var task = await _taskManagementDbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId);

        var priority = await _taskManagementDbContext.Priorities
            .FirstOrDefaultAsync(s => s.Id == inputModel.PriorityId);

        if (task == null || priority == null)
        {
            return false;
        }

        task.PriorityId = priority.Id;
        await _taskManagementDbContext.SaveChangesAsync();

        return true;
    }

    // Get all tasks which have not been notified and are due. This is
    // used by the background processing service to retrieve a list of
    // tasks which should be notified because the tasks are due for
    // completion

    // TODO:
    // dont return notifications that have last notific. sent less than 24 hours ago
    public async Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        var task = await _taskManagementDbContext.Tasks
            .Include(t => t.Notification)
            .Include(t => t.Status)
            .Include(t => t.AssignedTo)
            .Where(t => t.DueDate < DateTime.UtcNow &&
                (t.Notification.DueDateNotificationSent == false && //TODO: change to ||
                t.Notification.DayAfterNotificationSent == false) &&
                t.AssignedToId != null &&
                t.IsArchived == false)
            .ToListAsync();

        return task.Select(t => new TaskWithNotificationDto
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status.Name,
            DueDate = t.DueDate,
            AssignedToUser = t.AssignedTo.EmailAddress,
            Notification = new TaskNotificationDto
            {
                Id = t.Notification.Id,
                TaskId = t.Id,
                DueDateNotificationSent = t.Notification.DueDateNotificationSent,
                DayAfterNotificationSent = t.Notification.DayAfterNotificationSent,
                LastNotificationDate = t.Notification.LastNotificationDate
            },
            AssignedTo = new UserDto
            {
                Id = t.AssignedTo.Id,
                FullName = t.AssignedTo.FullName,
                Email = t.AssignedTo.EmailAddress,
                ProfileImageUrl = t.AssignedTo.ProfileImageUrl
            }
        });
    }

    // Updates the status of the task notifications, after the emails have
    // been sent. To ensure the emails will not be sent during the next
    // process, each process is executed every 30 minutes, the
    // notifications should be marked as completed

    // _dbcontext request sem sækir bara öll tasks sem eru 
    // með due date í dag og í gær. (Og duedatenotification /dayafternotification = false)

    // Date þarf að vera universal time
    public async Task UpdateTaskNotifications()
    {
        var tasks = await _taskManagementDbContext.Tasks
            .Include(t => t.Notification)
            .Where(t => t.DueDate < DateTime.UtcNow &&
            (t.Notification.DueDateNotificationSent == false ||
            t.Notification.DayAfterNotificationSent == false))
            .ToListAsync();
    
        foreach (var task in tasks)
        {
            if (task.Notification.DueDateNotificationSent == false)
            {
                task.Notification.DueDateNotificationSent = true;
            } else
            {
                task.Notification.DayAfterNotificationSent = true;
            }
            task.Notification.LastNotificationDate = DateTime.UtcNow;
        }
        await _taskManagementDbContext.SaveChangesAsync();
    }
}