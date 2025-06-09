using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.RolesVM
{
    public class NewRoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
