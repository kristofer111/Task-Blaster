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

            if (authUser != null)
            {
                foreach (var claim in authUser.Claims)
                {
                    if (claim.Type == $"{Namespace}email")
                    {
                        Console.WriteLine($"email claim value: {claim.Value}");
                        Console.WriteLine(claim.Value);
                        return claim.Value;
                    }
                }
            }
            return null;
            // var emailClaim = authUser?.Claims.FirstOrDefault(c => c.Type == $"{Namespace}email")?.Value ?? "null";
            // Console.WriteLine("eailClaim:", emailClaim);
            // return emailClaim;
        }
    }
}