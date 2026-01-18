using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Controllers;

// [Authorize]
[Route("[controller]")]
[ApiController]
public class PrioritiesController : ControllerBase
{
    private readonly IPriorityService _priorityService;

    public PrioritiesController(IPriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<PriorityDto>>> GetAllPriorities()
    {
        IEnumerable<PriorityDto> priorities = await _priorityService.GetAllPrioritiesAsync();
        return Ok(priorities);
    }
}