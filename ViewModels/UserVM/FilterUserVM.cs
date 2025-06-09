//////////////////////////////////
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.ViewModels.UserVM
{
    public class FilterUserVM
    {
        public string? SearchTerm { get; set; }
        public string? Role { get; set; }
        public List<SelectListItem>? Roles { get; set; }
        public List<UserVM> Users { get; set; } = new List<UserVM>();
    }
}
