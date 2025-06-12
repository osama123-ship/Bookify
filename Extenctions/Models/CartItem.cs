namespace Bookify.Extenctions.Models
{
    public class CartItem
    {
        public int EventId { get; set; }
        public int Quantity { get; set; }
        public int TicketTypeId { get; set; }
        public int PricePerTicket { get; set; }
        public string EventName { get; set; }
        public string TicketTypeName { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is CartItem other)
            {
                return EventId == other.EventId &&
                       Quantity == other.Quantity &&
                       TicketTypeId == other.TicketTypeId &&
                       PricePerTicket == other.PricePerTicket;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EventId, Quantity, TicketTypeId, PricePerTicket);
        }
    }
}
