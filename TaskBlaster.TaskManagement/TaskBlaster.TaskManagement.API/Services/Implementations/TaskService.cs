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


    public async Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        return await _taskRepository.GetPaginatedTasksByCriteriaAsync(query);
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

    public async Task<bool> ArchiveTaskByIdAsync(int taskId)
    {
        return await _taskRepository.ArchiveTaskByIdAsync(taskId);
    }

    public async Task<bool> AssignUserToTaskAsync(int taskId, int userId)
    {
        return await _taskRepository.AssignUserToTaskAsync(taskId, userId);
    }

    public async Task<bool> UnassignUserFromTaskAsync(int taskId, int userId)
    {
        return await _taskRepository.UnassignUserFromTaskAsync(taskId, userId);
    }

    public async Task<bool> UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
    {
        return await _taskRepository.UpdateTaskStatusAsync(taskId, inputModel);
    }

    public Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}