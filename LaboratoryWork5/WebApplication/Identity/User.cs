namespace WebApplication.Identity;


public class User
{
    public required string UserName { get; set; }

    public required string FullName { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }
}