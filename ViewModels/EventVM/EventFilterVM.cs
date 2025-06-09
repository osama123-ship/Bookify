using Bookify.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.ViewModels.EventVM
{
    public class EventFilterVM
    {
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? Date { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public List<DisplayForAdminVM> Events { get; set; }
    }
}
