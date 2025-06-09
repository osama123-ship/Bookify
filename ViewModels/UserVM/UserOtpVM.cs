using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.UserVM
{
    public class UserOtpVM
    {
        [Required, StringLength(1)]
        public string Digit1 { get; set; }

        [Required, StringLength(1)]
        public string Digit2 { get; set; }

        [Required, StringLength(1)]
        public string Digit3 { get; set; }

        [Required, StringLength(1)]
        public string Digit4 { get; set; }

        public string otp => $"{Digit1}{Digit2}{Digit3}{Digit4}";
    }
}
