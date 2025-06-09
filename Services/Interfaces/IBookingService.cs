using Bookify.Models;
using Bookify.ViewModels.BookingVM;

namespace Bookify.Services.Interfaces
{
    public interface IBookingService : IBaseService<Booking>
    {
        Task<List<BookingListItemVM>> GetAllBookingsAsync(string? userName = null);
    }
}
