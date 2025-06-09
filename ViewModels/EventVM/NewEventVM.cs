using Bookify.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModels.EventVM
{
    public class NewEventVM : IValidatableObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartBookingTime { get; set; }
        [Required]
        public DateTime EndBookingTime { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndBookingTime <= StartBookingTime)
            {
                yield return new ValidationResult(
                    "Start Booking Time should be less than End Booking Time",
                    new[] { nameof(StartBookingTime) });
            }

            TimeSpan def = StartTime - EndBookingTime;
            if (def.TotalDays < 2)
            {
                yield return new ValidationResult(
                    "Start Time must be at least 2 days after End Booking Time",
                    new[] { nameof(StartTime) });
            }

            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "Start Time should be less than End Time",
                    new[] { nameof(StartTime) });
            }
        }

    }
}
