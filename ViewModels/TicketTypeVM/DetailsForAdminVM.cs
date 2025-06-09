namespace Bookify.ViewModels.TicketTypeVM
{
    public class DetailsForAdminVM
    {
        public string Name { get; set; }
        public int TicketTypeId { get; set; }
        public int EventId { get; set; }
        public int TotalTickets { get; set; }
        public int ConfirmedTickets { get; set; }
        public int AvailableTickets { get; set; }
        public int Price { get; set; }
    }
}
