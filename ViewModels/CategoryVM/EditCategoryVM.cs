//////////////////////////////////
using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.CategoryVM
{
    public class EditCategoryVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
