using System.Security.Claims;

namespace Ant.Todos.Api.Authentication
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity?.Name ?? "unknown";
        }
    }
}