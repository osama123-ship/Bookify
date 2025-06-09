using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.UserVM
{
    public class UserLoginVM
    {
        [Required]
        public string EmailOrUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
