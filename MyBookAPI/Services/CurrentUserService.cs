using IdentityModel;
using Microsoft.AspNetCore.Http;
using MyBookAPI.Application.Common.Interfaces;
using System.Security.Claims;

namespace MyBookAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email { get; set; }
        public bool Authenticated { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var email = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Email);

            Email = email;

            Authenticated = !string.IsNullOrEmpty(email);
        }
    }
}
