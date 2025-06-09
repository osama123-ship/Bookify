using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.UserVM
{
    public class UserForgetPasswordVM
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email is inValid")]
        public string Email { get; set; }
    }
}
