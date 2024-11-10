using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<StatusDto>>> GetAllStatuses()
    {
        IEnumerable<StatusDto> statuses = await _statusService.GetAllStatusesAsync();
        return Ok(statuses);
    }
}