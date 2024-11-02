using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    /// <summary>
    /// Returns a list of all statuses
    /// </summary>
    /// <returns>A list of all statuses</returns>
    [HttpGet("")]
    public Task<ActionResult<IEnumerable<StatusDto>>> GetAllStatuses()
    {
        throw new NotImplementedException();
    }
}