using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public TaskRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }

    public Task ArchiveTaskByIdAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task AssignUserToTaskAsync(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateNewTaskAsync(TaskInputModel task)
    {
        var newTask = new Entities.Task
        {
            Title = task.Title,
            Description = task.Description,
            CreatedAt = DateTime.UtcNow,
            DueDate = task.DueDate,
            PriorityId = task.PriorityId,
            StatusId = task.StatusId,
            AssignedToId = null,
            CreatedById = null,
        };

        await _taskManagementDbContext.Tasks.AddAsync(newTask);
        _taskManagementDbContext.SaveChanges();

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