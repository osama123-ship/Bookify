using Bookify.Extenctions;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TicketTypeVM;

namespace Bookify.Services.Implementations
{
    public class TicketTypeService :BaseService<TicketType>, ITicketTypeService
    {
        private readonly ITicketTypeRepository ticketTypeRepository;
        private readonly ITemporaryBookingRepository temporaryRepository;
        private readonly ITicketRepository ticketRepository;

        public TicketTypeService(ITicketTypeRepository ticketTypeRepository, ITemporaryBookingRepository temporaryRepository,
            IBaseRepository<TicketType> repository,ITicketRepository ticketRepository) : base(repository) 
        {
            this.ticketTypeRepository = ticketTypeRepository;
            this.temporaryRepository = temporaryRepository;
            this.ticketRepository = ticketRepository;
        }
        public async Task AddAsync(NewTicketTypeVM New,int EventId)
        {
            TicketType ticketType = new TicketType(New.Name,New.Price,New.TotalTickets,EventId);
            await ticketTypeRepository.AddAsync(ticketType);
            await ticketTypeRepository.SaveChangesAsync();
        }
        public async Task<List<TicketType>> GetByEventIdAsync(int EventId)
        {
            var tickets = await ticketTypeRepository.GetByEventIdAsync(EventId);
            return tickets;
        }
        public async Task<List<DetailsForAdminVM>> TicketTypeDetailsForAdmin(int EventId)
        {
            List<TicketType> tickets = await ticketTypeRepository.GetByEventIdAsync(EventId);
            List< DetailsForAdminVM> VM = new List<DetailsForAdminVM>();
            foreach (var t in tickets)
            {
                int ConfirmedTickets = await ticketRepository.CountConfirmedTicketsAsync(t.Id);
                int TemporaryTickets = temporaryRepository.Count(t.Id);
                VM.Add(new DetailsForAdminVM
                {
                    TicketTypeId = t.Id,
                    EventId = EventId,
                    Name = t.Name,
                    TotalTickets = t.TotalTickets,
                    ConfirmedTickets = ConfirmedTickets,
                    AvailableTickets = t.TotalTickets - ConfirmedTickets - TemporaryTickets,
                    Price = t.Price
                });
            }
            return VM;
        }

    }
}

