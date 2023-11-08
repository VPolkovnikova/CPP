using System.Collections.Immutable;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using IdentityServer.Services;

namespace IdentityServer;


public class IdentityConfig
{
    public static ImmutableArray<IdentityResource> IdentityResources { get; } =
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
        {
            UserClaims =
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.PhoneNumber,
                JwtClaimTypes.Email,
                AdditionalUserClaimTypes.FullName
            }
        }
    ];


    public static ImmutableArray<Client> Clients { get; } =
    [
        new Client()
        {
            ClientId = "web-application",
            ClientSecrets =
            {
                new("web-application-secret".Sha256())
            },
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            },
            RedirectUris =
            {
                "http://localhost:5002/signin-oidc"
            },
            PostLogoutRedirectUris =
            {
                "http://localhost:5002/signout-callback-oidc"
            }
        }
    ];
}