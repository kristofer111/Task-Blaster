using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Controllers;

// [Authorize]
[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IMailService _mailService;

    public NotificationsController(IMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("emails/basic")]
    public async Task<ActionResult> SendBasicEmail([FromBody] BasicEmailInputModel inputModel)
    {
        EmailContentType contentType = inputModel.IsHtml ? EmailContentType.Html : EmailContentType.Text;

        var success = await _mailService.SendBasicEmailAsync(inputModel, contentType);

        if (!success)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("emails/template")]
    public Task<ActionResult> SendTemplatedEmail([FromBody] TemplateEmailInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}