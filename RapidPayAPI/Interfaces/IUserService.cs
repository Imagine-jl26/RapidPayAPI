using System.Security.Claims;

namespace RapidPayAPI.Interfaces
{
    public interface IUserService
    {
        string GetUserIdFromToken(ClaimsPrincipal user);
    }
}
