using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.UserVM
{
    public class UserNewPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}
