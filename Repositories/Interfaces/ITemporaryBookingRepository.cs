using Bookify.Extenctions.Models;
using Bookify.Models;

namespace Bookify.Repositories.Interfaces
{
    public interface ITemporaryBookingRepository:IBaseRepository<TemporaryBooking>
    {
        int Count(int id);
        List<CartItem> GetByUserId(string UserId);
        Task<bool> AddTempAsync(TemporaryBooking tempBooking);
        Task<TemporaryBooking> GetByEventIdAndTicketTypeIdAsync(int EventId, int ticketTypeId);
        Task Remove(IQueryable<TemporaryBooking> temporaryBooking);
        Task<TemporaryBooking?> GetByUserEventAndTicketTypeAsync(string userId, int eventId, int ticketTypeId);
        IQueryable<TemporaryBooking> GetSingleTempBookingAsync(string userId, int eventId, int ticketTypeId);
        Task RemoveAllByUserIdAsync(string userId);
        Task<List<TemporaryBooking>> GetAllByEventAndTicketTypeAsync(int EventId, int TicketTypeId);

    }
}