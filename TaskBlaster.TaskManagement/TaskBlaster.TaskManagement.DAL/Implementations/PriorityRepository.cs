using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class PriorityRepository : IPriorityRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public PriorityRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }


    public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        return await _taskManagementDbContext.Priorities.Select(p => new PriorityDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description
        }).ToListAsync();
    }
}