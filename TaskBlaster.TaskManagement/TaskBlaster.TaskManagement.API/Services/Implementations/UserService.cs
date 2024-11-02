using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsers();
        return users;
    }

    public async Task<int?> CreateUserIfNotExistsAsync(UserInputModel inputModel)
    {
        var newId = await _userRepository.CreateUserIfNotExists(inputModel);
        return newId;
    }

    public Task<UserDto?> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}