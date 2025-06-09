using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.TicketTypeVM
{
    public class NewTicketTypeVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number.")]
        public int Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number.")]
        public int TotalTickets { get; set; }
        public int EventId { get; set; }
    }
}
