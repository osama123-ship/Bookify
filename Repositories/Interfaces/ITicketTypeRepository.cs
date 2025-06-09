using Bookify.Models;

namespace Bookify.Repositories.Interfaces
{
    public interface ITicketTypeRepository:IBaseRepository<TicketType>
    {
        Task<List<TicketType>> GetByEventIdAsync(int EventId);
    }
}
