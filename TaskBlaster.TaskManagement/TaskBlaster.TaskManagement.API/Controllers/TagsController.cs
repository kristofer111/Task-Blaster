using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    /// <summary>
    /// Gets all tags
    /// </summary>
    /// <returns>A list of all tags</returns>
    [HttpGet("")]
    public Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create a new tag
    /// </summary>
    /// <param name="inputModel">An input model used to populate the new tag</param>
    [HttpPost("")]
    public Task<ActionResult> CreateNewTag([FromBody] TagInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}