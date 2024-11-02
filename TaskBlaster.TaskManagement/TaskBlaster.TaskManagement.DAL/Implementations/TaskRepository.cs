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
        _taskManagementDbContext.Tasks.Add(new Entities.Task
        {
            Title = task.Title,
            Description = task.Description,
            CreatedAt = DateTime.UtcNow,
            DueDate = task.DueDate,
            PriorityId = task.PriorityId,
            StatusId = 1,
        });

        throw new NotImplementedException();
    }

    public Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
    {
        throw new NotImplementedException();
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