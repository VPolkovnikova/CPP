namespace WebApplication.Models.Account;


public static class Information
{
    public class Output
    {
        public required string UserName { get; set; }

        public required string FullName { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Email { get; set; }
    }
}