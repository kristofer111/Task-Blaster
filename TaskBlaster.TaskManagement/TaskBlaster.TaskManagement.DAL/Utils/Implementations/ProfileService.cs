using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserEmail()
        {
            // var emailClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email);
            // return emailClaim?.Value;
            var authUser = _httpContextAccessor.HttpContext?.User;
            return authUser?.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "";
        }
    }
}