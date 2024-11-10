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

    public async Task CreateUserIfNotExistsAsync(UserInputModel inputModel)
    {
        await _userRepository.CreateUserIfNotExists(inputModel);
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }
}