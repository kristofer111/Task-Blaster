using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskBlaster.TaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private const string Namespace = "https://task-management-web-api.com";

        [HttpGet("")]
        public IActionResult Profile()
        {
            return Ok(new
            {
                Name = User.Claims.FirstOrDefault(c => c.Type == $"{Namespace}name")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == $"{Namespace}email")?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == $"{Namespace}picture")?.Value
            });
        }
    }
}