using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Address { get; set; }
        public Company? Company { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<TemporaryBooking> Temporaries { get; set; }

    }
}
