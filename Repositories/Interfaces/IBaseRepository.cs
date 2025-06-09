using Bookify.Data;
using Bookify.Extenctions.Models;
using Bookify.Models;
using Bookify.ViewModels.BookingDetailsVM;
using Bookify.ViewModels.EventVM;
using Bookify.ViewModels.TicketVM;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bookify.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteByIdAsync(int id);
        Task AddAsync(T obj);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
        Task<List<TicketVM>> GetTicketsByUserIdAsync(string UserId);
        Task<int> GetCompanyIdByUserIdAsync(string UserId);
        Task MakeBookingProcessAsync(string UserId, List<CartItem> cartItems);
        Task<int> CountTicketsForUser(string UserId, int EventId);
        Task AddCompanyAsync(Company company);
        Task<EventFilterVM> FilterEvents(int? CategoryId, int? CompanyId, DateTime? date);
        Task<List<BookingDetailVM>> GetBookingDetails(int BookingId);
    }
}
