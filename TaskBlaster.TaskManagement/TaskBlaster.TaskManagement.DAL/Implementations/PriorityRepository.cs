using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class PriorityRepository : IPriorityRepository
{
    public Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        throw new NotImplementedException();
    }
}