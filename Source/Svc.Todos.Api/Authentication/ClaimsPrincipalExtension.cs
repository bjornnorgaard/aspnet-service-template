using System.Security.Claims;

namespace Svc.Todos.Api.Authentication
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity?.Name ?? "jdoe";
        }
    }
}
