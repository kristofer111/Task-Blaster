using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class StatusRepository : IStatusRepository
{
    public Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
    {
        throw new NotImplementedException();
    }
}