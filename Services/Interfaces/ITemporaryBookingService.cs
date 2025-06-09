using Bookify.Models;
using Bookify.ViewModels.TemporaryBookingVM;

namespace Bookify.Services.Interfaces
{
    public interface ITemporaryBookingService : IBaseService<TemporaryBooking>
    {
        Task<List<BookTemporaryVM>> GetTicketsTypeVM(int EventId);
        Task<bool> BookingTemporary(BookTemporaryVM temporaryVM, string UserId, int eventId);
        Task RemoveAllByUserIdAsync(string userId);
        Task RemoveSingleByUserAsync(string userId, int eventId, int ticketTypeId);
        Task<List<TemporaryToViewVM>> GetAllByEventAndTicketTypeAsync(int EventId, int TicketTypeId, string? UserName);
    }
}