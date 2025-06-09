namespace Bookify.Models
{
    public class TemporaryBooking
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? EventId { get; set; }
        public string UserId { get; set; }
        public int TicketTypeId { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public ApplicationUser User { get; set; }
        public TicketType TicketType { get; set; }
    }
}
