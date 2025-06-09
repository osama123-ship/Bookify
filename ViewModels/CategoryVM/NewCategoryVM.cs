using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.CategoryVM
{
    public class NewCategoryVM
    {
        [Required] 
        public string Name { get; set; }
    }
}
