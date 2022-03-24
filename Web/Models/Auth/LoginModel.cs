using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Auth
{
    public class LoginModel
    {
        [Required, EmailAddress, DisplayName("Email")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(6), DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Keep me signed in")] public bool KeepMeSignedIn { get; set; }
    }
}