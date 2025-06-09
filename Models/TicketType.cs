
using Bookify.Repositories.Interfaces;

namespace Bookify.Models
{
    public class TicketType
    {
        public TicketType()
        {
            ConfirmedTickets = 0;

        }
        public TicketType(string Name, int Price, int TotalTickets, int EventId)
        {
            this.Name = Name;
            this.Price = Price;
            this.TotalTickets = TotalTickets;
            this.EventId = EventId;
            ConfirmedTickets = 0;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int TotalTickets { get; set; }
        public int ConfirmedTickets { get; set; }

        //Foreign Keys
        public int EventId { get; set; }
        public Event Event { get; set; }
        public ICollection<TemporaryBooking> Temporaries { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
