using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class Event
    {
        public Event()
        {
            Sales = 0;   
        }
        public Event(string Title,string Description,DateTime StartBookingTime, DateTime EndBookingTime, DateTime StartTime,
             DateTime EndTime, string Location, int CategoryId,int CompanyId)
        {
            this.Title = Title;
            this.Description = Description;
            this.StartBookingTime = StartBookingTime;
            this.EndBookingTime = EndBookingTime;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Location = Location;
            this.CategoryId = CategoryId;
            this.CompanyId = CompanyId;
            Sales = 0;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartBookingTime { get; set; }
        public DateTime EndBookingTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public long Sales { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<TicketType> TicketTypes { get; set; }    
    }
}
