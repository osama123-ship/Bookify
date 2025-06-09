using Bookify.Models;
using Bookify.Repositories.Implementations;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.BookingVM;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Services.Implementations
{
    public class BookingService : BaseService<Booking>, IBookingService
    {
        private readonly BaseRepository<Booking> _repository;

        public BookingService(IBaseRepository<Booking> repository) : base(repository)
        {
            _repository = (BaseRepository<Booking>)repository;
        }

        public async Task<List<BookingListItemVM>> GetAllBookingsAsync(string? userName = null)
        {
            IQueryable<Booking> query = _repository.GetBookingsWithUsers()
               .Include(b => b.BookingDetails).Include(b => b.User);


            if (!string.IsNullOrEmpty(userName))
                query = query.Where(b => b.User.UserName.Contains(userName));
            

            var bookings = await query.Select(b => new BookingListItemVM
            {
                Id = b.Id,
                UserId = b.User.Id,
                UserName = b.User.UserName,
                BookingDate = b.BookingDate
            }).OrderByDescending(b => b.BookingDate).ToListAsync();

            return bookings;
        }
    }
}
