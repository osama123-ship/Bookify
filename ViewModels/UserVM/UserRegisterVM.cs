using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.UserVM
{
    public class UserRegisterVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       // [Required]
      //  [DataType(DataType.Password)]
     //   [Compare("Password")]
        public string? ConfirmedPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^01\d{9}$", ErrorMessage = "PhoneNumber Should start with 01 and be 11 digit")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
