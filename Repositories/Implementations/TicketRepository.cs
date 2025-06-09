using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
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
    }
}
