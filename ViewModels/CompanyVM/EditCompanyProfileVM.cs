using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.CompanyVM
{
    public class EditCompanyProfileVM
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "companu name is req")]
        public string CompanyName { get; set; }

        [Url(ErrorMessage = "Enter Correct URL")]
        public string? Website { get; set; }
    }
}
