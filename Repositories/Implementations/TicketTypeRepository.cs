using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class TicketTypeRepository : BaseRepository<TicketType> , ITicketTypeRepository
    {
        public TicketTypeRepository(Context context) : base(context)
        {
        }
        public async Task<List<TicketType>>GetByEventIdAsync(int EventId)
        {
            return await context.TicketTypes.AsNoTracking().Where(t => t.EventId == EventId).ToListAsync();
        }
    }
}
