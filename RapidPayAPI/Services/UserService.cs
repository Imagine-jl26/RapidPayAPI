using RapidPayAPI.Interfaces;
using System.Security.Claims;

namespace RapidPayAPI.Services
{
    public class UserService : IUserService
    {
        public string GetUserIdFromToken(ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
