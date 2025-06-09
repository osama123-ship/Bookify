////////////////////////////
using System.ComponentModel.DataAnnotations;
namespace Bookify.ViewModels.RolesVM
{
    public class EditRoleVM
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
