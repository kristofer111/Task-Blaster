using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class StatusRepository : IStatusRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public StatusRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }


    public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
    {
        return await _taskManagementDbContext.Statuses.Select(s => new StatusDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description
        }).ToListAsync();
    }
}