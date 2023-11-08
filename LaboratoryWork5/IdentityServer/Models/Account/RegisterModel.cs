using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models.Account;


public static class RegisterModel
{
    public class PostInput
    {
        [MaxLength(50)]
        public required string UserName { get; init; }

        [MaxLength(500)]
        public required string FullName { get; init; }

        [Length(8, 16)]
        public required string Password { get; init; }

        [Compare(nameof(Password))]
        public required string ConfirmationPassword { get; init; }

        [RegularExpression(@"\+380\d{9}", ErrorMessage = "Invalid Ukrainian phone number.")]
        public required string PhoneNumber { get; init; }

        [EmailAddress]
        public required string Email { get; init; }


        public Output ToOutput() => new()
        {
            UserName = UserName,
            FullName = FullName,
            Password = Password,
            ConfirmationPassword = ConfirmationPassword,
            PhoneNumber = PhoneNumber,
            Email = Email
        };
    }


    public class Output
    {
        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Password { get; set; }

        public string? ConfirmationPassword { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}