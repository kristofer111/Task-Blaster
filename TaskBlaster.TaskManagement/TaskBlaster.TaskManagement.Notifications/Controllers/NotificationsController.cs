using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IMailService _mailService;

    public NotificationsController(IMailService mailService)
    {
        _mailService = mailService;
    }

    /// <summary>
    /// Sends a basic email
    /// </summary>
    /// <param name="inputModel">An input model used to populate the basic email</param>
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

    /// <summary>
    /// Sends a templated email (optional)
    /// </summary>
    /// <param name="inputModel">An input model used to populate the templated email</param>
    [HttpPost("emails/template")]
    public Task<ActionResult> SendTemplatedEmail([FromBody] TemplateEmailInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}