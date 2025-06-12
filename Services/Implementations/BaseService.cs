using Bookify.Extenctions.Models;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.ViewModels.EventVM;
using Bookify.ViewModels.TicketVM;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.BookingDetailsVM;
using Bookify.Repositories.Implementations;

namespace Bookify.Services.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> repository;
        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }
        public async Task<List<TicketVM>> GetTicketsByUserIdAsync(string UserId,int? TicketTypeId)
        {
            return await repository.GetTicketsByUserIdAsync(UserId, TicketTypeId);
        }
        public async Task MakeBookingProcessAsync(string UserId, List<CartItem> cartItems)
        {
            await repository.MakeBookingProcessAsync(UserId, cartItems);
        }
        public async Task<int> CountTicketsForUser(string UserId,int EventId)
        {
            return await repository.CountTicketsForUser(UserId, EventId);
        }
        public async Task<EventFilterVM> DisplayForAdminAsync(int? CategoryId, int? CompanyId, DateTime? Date)
        {
            return await repository.FilterEvents(CategoryId, CompanyId, Date);
        }
        public async Task<List<BookingDetailVM>> GetBookingDetails(int BookingId)
        {
            return await repository.GetBookingDetails(BookingId);
        }
        public async Task<string> GetTicketTypeName(int TicketTypeId)
        {
            return await repository.GetTicketTypeName(TicketTypeId);
        }

    }
}
