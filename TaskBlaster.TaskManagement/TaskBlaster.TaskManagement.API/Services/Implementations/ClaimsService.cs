using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Services.Implementations
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string Audience = "https://task-management-web-api.com";

        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string RetrieveUserEmailClaim()
        {
            var authUser = _httpContextAccessor.HttpContext?.User;
            return authUser?.Claims.FirstOrDefault(c => c.Type == $"{Audience}email")?.Value ?? "";
        }
        public string RetrieveUserNameClaim()
        {
            var authUser = _httpContextAccessor.HttpContext?.User;
            return authUser?.Claims.FirstOrDefault(c => c.Type == $"{Audience}name")?.Value ?? "";
        }
    }
}