using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
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
                User.Identity?.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == $"{Namespace}email")?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
    }
}