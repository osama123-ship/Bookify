namespace Bookify.ViewModels.BookingDetailsVM
{
    public class BookingDetailVM
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public int PricePerTicket { get; set; }
        public int Quantity { get; set; }
        public string EventName { get; set; }
        public string CategoryName { get; set; }
        public string TicketTypeName { get; set; }
    }
}
