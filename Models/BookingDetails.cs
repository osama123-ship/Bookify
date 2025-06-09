
namespace Bookify.Models
{
    public class BookingDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
