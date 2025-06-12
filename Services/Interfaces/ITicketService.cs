using Bookify.Models;
using Bookify.ViewModels.TicketVM;

namespace Bookify.Services.Interfaces
{
    public interface ITicketService:IBaseService<Ticket>
    {
        Task<List<ConfirmedTicketsVM>> GeTicketsByTicketType(int TicketTypeId, string? UserName);
    }
}