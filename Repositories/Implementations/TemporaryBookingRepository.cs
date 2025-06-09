using Bookify.Data;
using Bookify.Extenctions.Models;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class TemporaryBookingRepository : BaseRepository<TemporaryBooking>, ITemporaryBookingRepository
    {
        public TemporaryBookingRepository(Context context) : base(context)
        {
         
        }
        public int Count(int id)
        {
            return context.Temporaries.AsNoTracking().Where(t => t.TicketTypeId == id && t.ExpiresAt > DateTime.UtcNow).Sum(t => t.Quantity);
        }
        public List<CartItem> GetByUserId(string UserId)
        {
        var groupedTempTickets = context.Temporaries
        .Where(tb => tb.UserId == UserId && tb.ExpiresAt > DateTime.UtcNow)
        .GroupBy(tb => tb.TicketTypeId).Select(g => new CartItem
        {TicketTypeId = g.Key, Quantity = g.Sum(tb => tb.Quantity) }).ToList();
            return groupedTempTickets;
        }
        public async Task<bool> AddTempAsync(TemporaryBooking tempBooking)
        {
            using var transaction = await BeginTransactionAsync();
            var ticketType = await context.TicketTypes
            .FromSqlRaw("SELECT * FROM TicketTypes WITH (UPDLOCK, ROWLOCK) WHERE Id = {0}", tempBooking.TicketTypeId)
            .FirstOrDefaultAsync();
            if (ticketType != null)
            {
                int cnt = ticketType.TotalTickets - ticketType.ConfirmedTickets - Count(ticketType.Id);
                if (cnt >= tempBooking.Quantity)
                {
                    await AddAsync(tempBooking);
                    await SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
            }
            await transaction.RollbackAsync();
            return false;
        }
        public async Task<TemporaryBooking> GetByEventIdAndTicketTypeIdAsync(int EventId, int ticketTypeId)
        {
            return await context.Temporaries.Where(t => t.EventId == EventId && t.TicketTypeId == ticketTypeId).FirstAsync();
        }
        public async Task<TemporaryBooking?> GetByUserEventAndTicketTypeAsync(string userId, int eventId, int ticketTypeId)
        {
            return await context.Temporaries
                .FirstOrDefaultAsync(t => t.UserId == userId && t.EventId == eventId && t.TicketTypeId == ticketTypeId);
        }

        public IQueryable<TemporaryBooking> GetSingleTempBookingAsync(string userId, int eventId, int ticketTypeId)
        {
            return context.Temporaries.Where(t => t.UserId == userId && t.EventId == eventId && t.TicketTypeId == ticketTypeId);
        }


        public async Task RemoveAllByUserIdAsync(string userId)
        {
            var userBookings = context.Temporaries.Where(t => t.UserId == userId);
            context.Temporaries.RemoveRange(userBookings);
            await context.SaveChangesAsync();
        }

        public async Task Remove(IQueryable<TemporaryBooking> temporaryBooking)
        {
            foreach (var temp in temporaryBooking)
                context.Temporaries.Remove(temp);

            await context.SaveChangesAsync();
        }
        public async Task<List<TemporaryBooking>> GetAllByEventAndTicketTypeAsync(int EventId,int TicketTypeId)
        {
            return await context.Temporaries.AsNoTracking().Where(t => t.EventId == EventId && t.TicketTypeId == TicketTypeId)
                .Include(t => t.User).ToListAsync();
        }
    }
}
