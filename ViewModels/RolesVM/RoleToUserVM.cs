using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.RolesVM
{
    public class RoleToUserVM
    {
        public RoleToUserVM()
        {
            
        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
