using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Route("[controller]")]
[ApiController]
public class PrioritiesController : ControllerBase
{
    /// <summary>
    /// Returns a list of all priorities
    /// </summary>
    /// <returns>A list of all priorities</returns>
    [HttpGet("")]
    public Task<ActionResult<IEnumerable<PriorityDto>>> GetAllPriorities()
    {
        throw new NotImplementedException();
    }
}