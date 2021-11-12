using System.Security.Claims;

namespace Api.Todos.Authentication
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity?.Name ?? "johndoe";
        }
    }
}