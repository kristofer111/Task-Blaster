using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }


    public Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        return task;
    }

    public async Task<int?> CreateNewTaskAsync(TaskInputModel task)
    {
        return await _taskRepository.CreateNewTaskAsync(task);
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