using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;

namespace Bookify.Services.Implementations
{
    public class BookingDetailService : BaseService<BookingDetails>, IBookingDetailService
    {
        public BookingDetailService(IBaseRepository<BookingDetails> repository) : base(repository) 
        {

        }
    }

}
