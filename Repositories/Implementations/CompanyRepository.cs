using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(Context context) : base(context)
        {
        }
        public async Task<List<Event>> GetEventsAsync(int id)
        {
            return await context.Events.Where(e => e.CompanyId == id).ToListAsync();
        }
        public async Task<Company?> GetByUserIdAsync(string userId)
        {
            return await context.Companies
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task UpdateAsync(Company company)
        {
            context.Companies.Update(company);
            await context.SaveChangesAsync();
        }
    }
}
