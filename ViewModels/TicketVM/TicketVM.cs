namespace Bookify.ViewModels.TicketVM
{
    public class TicketVM
    {
        public int Id { get; set; }
        public string TickeTypeName { get; set; }
        public int Price { get; set; }
        public string EventName { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public bool? State { get; set; }
    }
}
