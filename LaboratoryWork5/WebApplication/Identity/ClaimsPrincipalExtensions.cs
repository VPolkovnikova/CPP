using System.Diagnostics;
using System.Security.Claims;

namespace WebApplication.Identity;


public static class ClaimsPrincipalExtensions
{
    public static User GetUser(this ClaimsPrincipal principal) => new()
    {
        UserName = principal.FindFirstValue(UserClaimTypes.UserName) ?? throw new UnreachableException(),
        FullName = principal.FindFirstValue(UserClaimTypes.FullName) ?? throw new UnreachableException(),
        PhoneNumber = principal.FindFirstValue(UserClaimTypes.PhoneNumber) ?? throw new UnreachableException(),
        Email = principal.FindFirstValue(UserClaimTypes.Email) ?? throw new UnreachableException()
    };
}