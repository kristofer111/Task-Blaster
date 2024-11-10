using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Controllers;

// [Authorize]
[Route("[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
    {
        IEnumerable<TagDto> tags = await _tagService.GetAllTagsAsync();
        return Ok(tags);
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateNewTag([FromBody] TagInputModel inputModel)
    {
        var newId = await _tagService.CreateNewTagAsync(inputModel);

        if (newId == null)
        {
            return Conflict(new { message = "Name has already been taken" });
        }

        return StatusCode(201);
    }
}