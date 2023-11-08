using System.Security.Claims;
using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityServer.Services;


public class UserClaimsPrincipalFactory(
    UserManager<User> userManager,
    IOptions<IdentityOptions> optionsAccessor
) : UserClaimsPrincipalFactory<User>(userManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new(AdditionalUserClaimTypes.FullName, user.FullName));
        return identity;
    }
}


public static class AdditionalUserClaimTypes
{
    public const string FullName = "full_name";
}