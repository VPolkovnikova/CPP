namespace IdentityServer.Models.Account;


public static class LoginModel
{
    public class GetInput
    {
        public string? ReturnUrl { get; init; }


        public Output ToOutput() => new()
        {
            ReturnUrl = ReturnUrl
        };
    }


    public class PostInput
    {
        public required string UserName { get; init; }

        public required string Password { get; init; }

        public required bool IsPersistent { get; init; }

        public string? ReturnUrl { get; init; }


        public Output ToOutput() => new()
        {
            UserName = UserName,
            Password = Password,
            IsPersistent = IsPersistent,
            ReturnUrl = ReturnUrl
        };
    }


    public class Output
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public bool IsPersistent { get; set; }

        public string? ReturnUrl { get; set; }
    }
}