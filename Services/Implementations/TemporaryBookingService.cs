using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TemporaryBookingVM;


namespace Bookify.Services.Implementations
{
    public class TemporaryBookingService: BaseService<TemporaryBooking>, ITemporaryBookingService
    {
        protected readonly ITemporaryBookingRepository temporaryRepository;
        protected readonly ITicketTypeRepository ticketTypeRepository;
        public TemporaryBookingService(ITicketTypeRepository ticketTypeRepository, ITemporaryBookingRepository temporaryRepository,
            IBaseRepository<TemporaryBooking> repository) : base(repository)
        {
            this.ticketTypeRepository = ticketTypeRepository;
            this.temporaryRepository = temporaryRepository;
        }

        public async Task<List<BookTemporaryVM>> GetTicketsTypeVM(int EventId)
        {
            List<TicketType> Tickets = await ticketTypeRepository.GetByEventIdAsync(EventId);
            var VM = Tickets.Where(t => t.TotalTickets != t.ConfirmedTickets).Select(ticket => new BookTemporaryVM
            {
                TicketTypeId = ticket.Id,
                AvailableTickets = ticket.TotalTickets - ticket.ConfirmedTickets - (temporaryRepository.Count(ticket.Id)),
                Price = ticket.Price,
                TicketName = ticket.Name
            }).ToList();
            return VM;
        }
        public async Task<bool> BookingTemporary(BookTemporaryVM temporaryVM, string UserId,int eventId)
        {
            TemporaryBooking temporary = new TemporaryBooking
            {
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                UserId = UserId,
                TicketTypeId = temporaryVM.TicketTypeId,
                Quantity = temporaryVM.Quantity,
                EventId = eventId
            };
            return await temporaryRepository.AddTempAsync(temporary);
        }

        public async Task RemoveSingleByUserAsync(string userId, int eventId, int ticketTypeId)
        {
            var booking = temporaryRepository.GetSingleTempBookingAsync(userId, eventId, ticketTypeId);
            if (booking != null)
                await temporaryRepository.Remove(booking);

        }

        public async Task RemoveAllByUserIdAsync(string userId)
        {
            await temporaryRepository.RemoveAllByUserIdAsync(userId);
        }
        public async Task<List<TemporaryToViewVM>> GetAllByEventAndTicketTypeAsync(int EventId, int TicketTypeId, string? UserName)
        {
            var list = await temporaryRepository.GetAllByEventAndTicketTypeAsync(EventId, TicketTypeId);
            if (!string.IsNullOrEmpty(UserName))
                list = list.Where(t => t.User.UserName.Contains(UserName)) as List<TemporaryBooking>;
            
            return list.Select(item => new TemporaryToViewVM
            {
                Id = item.Id,
                ReservedAt = item.ReservedAt,
                ExpiresAt = item.ExpiresAt,
                Quantity = item.Quantity,
                UserName = item.User.UserName
            }).ToList();
        }

    }
}
