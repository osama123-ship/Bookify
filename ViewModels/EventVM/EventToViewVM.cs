using Bookify.Models;
using Bookify.ViewModels.TicketTypeVM;

namespace Bookify.ViewModels.EventVM
{
    public class EventToViewVM
    {
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartBookingTime { get; set; }
        public DateTime EndBookingTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public List<TicketsToViewVM>? TicketsToView { get; set; }
    }
}
