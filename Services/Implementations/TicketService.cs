using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TicketVM;

namespace Bookify.Services.Implementations
{
    public class TicketService:BaseService<Ticket>, ITicketService
    {
        private readonly ITicketRepository ticketRepository;

        public TicketService(IBaseRepository<Ticket> repository, ITicketRepository ticketRepository) : base(repository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<List<ConfirmedTicketsVM>> GeTicketsByTicketType(int TicketTypeId,string? UserName)
        {
            return await ticketRepository.GeTicketsByTicketType(TicketTypeId,UserName);
        }
    }
}
