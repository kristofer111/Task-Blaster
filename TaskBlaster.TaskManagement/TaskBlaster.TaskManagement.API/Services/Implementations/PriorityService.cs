using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class PriorityService : IPriorityService
{
    public Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        throw new NotImplementedException();
    }
}