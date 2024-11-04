using Microsoft.AspNetCore.Http;
using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string Namespace = "https://task-management-web-api.com";

        public ProfileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserEmail()
        {
            var authUser = _httpContextAccessor.HttpContext?.User;
            return authUser?.Claims.FirstOrDefault(c => c.Type == $"{Namespace}email")?.Value ?? "null";
        }
    }
}