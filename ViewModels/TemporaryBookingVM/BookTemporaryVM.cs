using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.TemporaryBookingVM
{
    public class BookTemporaryVM
    {
        public BookTemporaryVM() {
            remaningForUesr = 0;
            AvailableTickets = 0;
        }
        public int TicketTypeId { get; set; }
        public string? TicketName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public int AvailableTickets { get; set; }
        public int remaningForUesr { get; set; }
        public int Price { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Quantity > AvailableTickets)
                yield return new ValidationResult
                    ("Quantity can't be greater than AvailableTicket", [nameof(Quantity)]);

            if (Quantity > remaningForUesr)
                yield return new ValidationResult
                    ("Quantity can't be greater than AvailableTicket", [nameof(Quantity)]);
        }
    }
}
