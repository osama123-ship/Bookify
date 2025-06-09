using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? QRCode { get; set; }  
        public bool? Used { get; set; }  
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public int TicketTypeId { get; set; }
        public int BookingDetailsId { get; set; }
        public BookingDetails BookingDetails { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("TicketTypeId")]
        public TicketType TicketType { get; set; }

    }
}
