using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class BookingDetailsRepository : BaseRepository<BookingDetails>, IBookingDetailsRepository
    {
        public BookingDetailsRepository(Context context) : base(context)
        {
        }

    }
}
