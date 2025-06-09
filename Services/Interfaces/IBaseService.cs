using Bookify.Extenctions.Models;
using Bookify.ViewModels.BookingDetailsVM;
using Bookify.ViewModels.EventVM;
using Bookify.ViewModels.TicketVM;

namespace Bookify.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<List<TicketVM>> GetTicketsByUserIdAsync(string UserId);
        Task MakeBookingProcessAsync(string UserId, List<CartItem> cartItems);
        Task<int> CountTicketsForUser(string UserId,int EventId);
        Task<EventFilterVM> DisplayForAdminAsync(int? CategoryId, int? CompanyId, DateTime? Date);
        Task<List<BookingDetailVM>> GetBookingDetails(int BookingId);
    }
}
