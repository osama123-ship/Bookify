using System.Security.Claims;

namespace Bookify.Extenctions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserIdFromClaim(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
