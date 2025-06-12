using Bookify.Models;
using Bookify.ViewModels.TicketVM;

namespace Bookify.Repositories.Interfaces
{
    public interface ITicketRepository:IBaseRepository<Ticket>
    {
        Task<List<Ticket>> GetTicketsByBookingDetails(int DetailsId);
        Task<List<ConfirmedTicketsVM>> GeTicketsByTicketType(int TicketTypeId, string? UserName);
        Task<int> CountConfirmedTicketsAsync(int TicketTypeId);
    }
}