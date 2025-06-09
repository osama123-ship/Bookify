using Bookify.Models;

namespace Bookify.Repositories.Interfaces
{
    public interface ICompanyRepository:IBaseRepository<Company>
    {
        Task<List<Event>> GetEventsAsync(int id);
        Task<Company?> GetByUserIdAsync(string userId);

        Task UpdateAsync(Company company);
    }
}
