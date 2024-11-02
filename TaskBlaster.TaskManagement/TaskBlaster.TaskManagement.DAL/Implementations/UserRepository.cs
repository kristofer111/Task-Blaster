using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class UserRepository : IUserRepository
{
    public Task CreateUserIfNotExists(UserInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}