using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Controllers;

// [Authorize]
[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets all registered users
    /// </summary>
    /// <returns>A list of all registered users</returns>
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return Conflict(new { Message = "Email address already taken" });
        }

        return Ok(user);
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateUserIfNotExistsAsync([FromBody] UserInputModel userInputModel)
    {
        await _userService.CreateUserIfNotExistsAsync(userInputModel);
        return Ok();
    }

}