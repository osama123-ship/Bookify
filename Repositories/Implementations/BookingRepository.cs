using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;

namespace Bookify.Repositories.Implementations
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(Context context) : base(context)
        {
        }
    }
}
