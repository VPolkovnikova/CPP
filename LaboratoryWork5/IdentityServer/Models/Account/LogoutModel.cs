namespace IdentityServer.Models.Account;


public static class LogoutModel
{
    public class GetInput
    {
        public string? LogoutId { get; init; }
    }
}