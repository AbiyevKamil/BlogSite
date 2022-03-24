using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Auth
{
    public class RegisterModel
    {
        [Required, EmailAddress, DisplayName("Email")]
        public string Email { get; set; }

        [Required, MaxLength(30), DisplayName("Username")]
        public string UserName { get; set; }

        [Required, RegularExpression(@"^\w+\s\w+\s?$", ErrorMessage = "At least two words needed."),
         DisplayName("Name, Surname")]
        public string FullName { get; set; }

        [Required, DataType(DataType.Text), DisplayName("Description")]
        public string Description { get; set; }

        [Required, DataType(DataType.Password), MinLength(6), DisplayName("Password")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords don't match"),
         DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}