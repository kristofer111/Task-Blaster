using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class TaskService : ITaskService
{
    public Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateNewTaskAsync(TaskInputModel task)
    {
        throw new NotImplementedException();
    }

    public Task ArchiveTaskByIdAsync(int taskId)
    {
        throw new NotImplementedException();
    }

    public Task AssignUserToTaskAsync(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task UnassignUserFromTaskAsync(int taskId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}