using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IClaimsService _claimsService;
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationService _notificationService;


    public TaskService(ITaskRepository taskRepository, IClaimsService claimsService, INotificationRepository notificationRepository, INotificationService notificationService)
    {
        _taskRepository = taskRepository;
        _claimsService = claimsService;
        _notificationRepository = notificationRepository;
        _notificationService = notificationService;
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
        string emailClaim = _claimsService.RetrieveUserEmailClaim();
        var newId = await _taskRepository.CreateNewTaskAsync(task, emailClaim);

        if (newId == null)
        {
            return null;
        }

        await _notificationRepository.CreateNewTaskNotificationAsync((int) newId);

        return newId;
    }

    public async Task<bool> ArchiveTaskByIdAsync(int taskId)
    {
        return await _taskRepository.ArchiveTaskByIdAsync(taskId);
    }

    public async Task<bool> AssignUserToTaskAsync(int taskId, int userId)
    {
        var success = await _taskRepository.AssignUserToTaskAsync(taskId, userId);
        
        if (!success)
        {
            return false;
        }

        // await _notificationService.SendAssignedNotification(userId, taskId);

        return success;
    }

    public async Task<bool> UnassignUserFromTaskAsync(int taskId, int userId)
    {
        var success = await _taskRepository.UnassignUserFromTaskAsync(taskId, userId);
    
        if (!success)
        {
            return false;
        }

        await _notificationService.SendUnassignedNotification(userId, taskId);

        return success;
    }

    public async Task<bool> UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
    {
        return await _taskRepository.UpdateTaskStatusAsync(taskId, inputModel);
    }

    public async Task<bool> UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        return await _taskRepository.UpdateTaskPriorityAsync(taskId, inputModel);
    }

    public Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        return _taskRepository.GetTasksForNotifications();
    }

    public async Task UpdateTaskNotifications()
    {
        await _taskRepository.UpdateTaskNotifications();
    }
}