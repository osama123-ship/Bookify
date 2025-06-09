using Bookify.Models;

namespace Bookify.Repositories.Interfaces
{
    public interface ITicketRepository:IBaseRepository<Ticket>
    {
        Task<List<Ticket>> GetTicketsByBookingDetails(int DetailsId);
    }
}