using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(Context context) : base(context)
        {
           
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }
        public async Task<List<Event>>GetByCategoryIdAsync(int id)
        {
            return await context.Events.AsNoTracking().Where(e => e.CategoryId == id).Include(e => e.TicketTypes).ToListAsync();
        }
        public async Task<List<Event>> GetAllWithCategoryAsync()
        {
            return await context.Events.AsNoTracking().Include(e => e.Category).ToListAsync();
        }

    }
}
