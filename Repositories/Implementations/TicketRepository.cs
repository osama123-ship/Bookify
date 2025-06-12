using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.ViewModels.TicketVM;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(Context context) : base(context)
        {
        }
        public async Task<List<Ticket>> GetTicketsByBookingDetails(int DetailsId)
        {
            return await context.Tickets.Where(t => t.BookingDetailsId == DetailsId).ToListAsync();
        }
        public async Task<List<ConfirmedTicketsVM>> GeTicketsByTicketType(int TicketTypeId, string? UserName)
        {
            var query = await context.Tickets.AsNoTracking().
             Where(t => t.TicketTypeId == TicketTypeId).
             Include(t => t.User).Include(t => t.TicketType).
             GroupBy(t => t.User.UserName).
             Select(g => new ConfirmedTicketsVM { UserName = g.Key, Quantity = g.Count(), TicketTypeId = TicketTypeId }).
             ToListAsync();
            if (!string.IsNullOrEmpty(UserName))
                query = query.Where(q => q.UserName.Contains(UserName)).ToList();

            return query;
        }
        public async Task<int> CountConfirmedTicketsAsync(int TicketTypeId)
        {
            return await context.Tickets.CountAsync(t=>t.TicketTypeId == TicketTypeId);
        }
    }
}
