using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    /// <summary>
    /// Gets all registered users
    /// </summary>
    /// <returns>A list of all registered users</returns>
    [HttpGet("")]
    public Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}