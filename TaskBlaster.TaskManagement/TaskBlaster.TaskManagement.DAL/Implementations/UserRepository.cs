using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Entities;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class UserRepository : IUserRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public UserRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }

    // Creates a user if it does not exist, otherwise does nothing. This is
    // used by the post login handler to make sure users are stored
    // within the database to keep track of users within the system
    public async Task CreateUserIfNotExists(UserInputModel inputModel)
    {
        bool userHasEmail = await _taskManagementDbContext.Users
            .AnyAsync(u => u.EmailAddress == inputModel.Email);

        if (!userHasEmail)
        {
            User newUser = new User
            {
                FullName = inputModel.FullName,
                EmailAddress = inputModel.Email,
                ProfileImageUrl = inputModel.ProfileImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _taskManagementDbContext.Users.AddAsync(newUser);
            await _taskManagementDbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        return await _taskManagementDbContext.Users.Select(u => new UserDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.EmailAddress,
            ProfileImageUrl = u.ProfileImageUrl
        }).ToListAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _taskManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.EmailAddress,
            ProfileImageUrl = user.ProfileImageUrl
        };

    }
}