using Bookify.Data;
using Bookify.Extenctions.Models;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.ViewModels.BookingDetailsVM;
using Bookify.ViewModels.EventVM;
using Bookify.ViewModels.TicketTypeVM;
using Bookify.ViewModels.TicketVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Protocol.Core.Types;
using System.Net.Sockets;

namespace Bookify.Repositories.Implementations
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly Context context;

        public BaseRepository(Context context)
        {
            this.context = context;
        }
        public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();
        public async Task DeleteByIdAsync(int id)
        {
            var obj = await GetByIdAsync(id);
            context.Set<T>().Remove(obj);
        }
        public async Task AddAsync(T obj)
        {
            await context.Set<T>().AddAsync(obj);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
           return await context.Database.BeginTransactionAsync();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task<List<TicketVM>>GetTicketsByUserIdAsync(string UserId,int? TicketTypeId)
        {
            List<TicketVM> ticketsVM = new List<TicketVM>();

            List<Booking> booking = await context.Bookings.AsNoTracking().Include(b => b.BookingDetails).Where(b => b.UserId == UserId).ToListAsync();          
            foreach (Booking b in booking)
            {
                List<BookingDetails> details = await context.BookingDetails.
                   AsNoTracking().Include(d => d.Tickets).Where(d => d.BookingId == b.Id).ToListAsync();
                 foreach(BookingDetails detail in details)
                {
                    TicketType ticketType = new TicketType();
                    Event @event = new Event();
                    bool ok = true;
                    foreach(Ticket ticket in detail.Tickets)
                    {
                        if(ok)
                        {
                            ticketType = await context.TicketTypes.AsNoTracking().Where(t => t.Id == ticket.TicketTypeId).FirstAsync();
                            if (TicketTypeId.HasValue)
                            {
                                if (ticketType.Id != TicketTypeId)
                                    break;
                            }
                            @event = await context.Events.AsNoTracking().Where(e => e.Id == ticketType.EventId).FirstAsync();
                            ok = false;
                        }
                        TicketVM vm = new TicketVM
                        {
                            Id = ticket.Id,
                            EventName = @event.Title,
                            TickeTypeName = ticketType.Name,
                            Price = ticketType.Price,
                            Venue = @event.Location,
                            Date = @event.StartTime,
                            State = ticket.Used
                        };
                        ticketsVM.Add(vm);
                    }
                }
            }
            return ticketsVM;
        }
        

        public async Task<int> GetCompanyIdByUserIdAsync(string UserId)
        {
            return (await context.Companies.AsNoTracking().Where(c => c.UserId == UserId).FirstAsync()).Id;
        }
        public async Task MakeBookingProcessAsync(string UserId, List<CartItem> cartItems)
        {
            using var transaction = await BeginTransactionAsync();
            try
            {
                Booking booking = new Booking { UserId = UserId, BookingDate = DateTime.Now };
                await context.Bookings.AddAsync(booking);
                await SaveChangesAsync();
                foreach (var cartItem in cartItems)
                {
                    BookingDetails details = new BookingDetails
                    {
                        Quantity = cartItem.Quantity,
                        TotalPrice = cartItem.Quantity * cartItem.PricePerTicket,
                        BookingId = booking.Id
                    };
                    Event @event = await context.Events.FirstAsync(e => e.Id == cartItem.EventId);
                    @event.Sales += cartItem.Quantity * cartItem.PricePerTicket;

                    await context.BookingDetails.AddAsync(details);
                    await SaveChangesAsync();

                    for (int i = 0; i < cartItem.Quantity; i++)
                    {
                        Ticket ticket = new Ticket
                        {
                            CreatedAt = DateTime.Now,
                            BookingDetailsId = details.Id,
                            TicketTypeId = cartItem.TicketTypeId,
                            Used = true,
                            UserId = UserId
                        };
                        await context.Tickets.AddAsync(ticket);
                    }
                    await SaveChangesAsync();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<int> CountTicketsForUser(string UserId, int EventId)
        {
            Event @event = await context.Events.AsNoTracking().Include(e => e.TicketTypes).Where(e => e.Id == EventId).FirstAsync();
            int cnt = 0;
            foreach (TicketType ticket in @event.TicketTypes)
                cnt += await context.Tickets.AsNoTracking().Where(t => t.UserId == UserId && t.TicketTypeId == ticket.Id).CountAsync();
            cnt += await context.Temporaries.AsNoTracking().Where(t => t.UserId == UserId && t.EventId == EventId).SumAsync(t => t.Quantity);
            return cnt;
        }
        public async Task AddCompanyAsync(Company company)
        {
            await context.Companies.AddAsync(company);
            await SaveChangesAsync();
        }

        public IQueryable<Booking> GetBookingsWithUsers()
        {
            return context.Bookings.Include(b => b.User).AsQueryable();
        }
        public async Task<EventFilterVM> FilterEvents(int? CategoryId, int? CompanyId, DateTime? date)
        {
            EventFilterVM show = new EventFilterVM();
            IEnumerable<Event> events = context.Events.AsNoTracking().Include(e => e.Category).Include(e => e.TicketTypes);
            if (CategoryId.HasValue)
                events = events.Where(e => e.CategoryId == CategoryId);
            if (CompanyId.HasValue)
                events = events.Where(e => e.CompanyId == CompanyId);

            if (date.HasValue)
                events = events.Where(e => e.StartTime >= date);

            show.Companies = await context.Companies.AsNoTracking().Include(c => c.User).Select(c => 
            new SelectListItem { Value = c.Id.ToString(), Text = c.User.UserName }).ToListAsync();
            show.Categories = await context.Categories.AsNoTracking().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToListAsync();
            show.Events = events.Select(e => new DisplayForAdminVM
            {
                EventId = e.Id,
                EventName = e.Title,
                CategoryId = e.CategoryId,
                CategoryName = e.Category.Name,
                Sales = e.TicketTypes.Sum(t => t.ConfirmedTickets * t.Price)
            }).ToList();
            return show;
        }

        public async Task<List<BookingDetailVM>> GetBookingDetails(int BookingId)
        {
            List<BookingDetailVM> show = new List<BookingDetailVM>();
            var BookingDetails = await context.BookingDetails.AsNoTracking().Where(b => b.BookingId == BookingId).ToListAsync();
            foreach (var booking in BookingDetails)
            {
                Ticket ticket = await context.Tickets.AsNoTracking().Where(t => t.BookingDetailsId == booking.Id).FirstAsync();
                TicketType ticketType = await context.TicketTypes.AsNoTracking().Where(t => t.Id == ticket.TicketTypeId).FirstAsync();
                Event @event = await context.Events.AsNoTracking().Where(e => e.Id == ticketType.EventId).Include(e => e.Category).FirstAsync();
                show.Add(new BookingDetailVM
                {
                    Id = booking.Id,
                    Quantity = booking.Quantity,
                    TotalPrice = booking.TotalPrice,
                    EventName = @event.Title,
                    CategoryName = @event.Category.Name,
                    TicketTypeName = ticketType.Name,
                    PricePerTicket = ticketType.Price
                });
            }
            return show;
        }
        public async Task<string> GetTicketTypeName(int TicketTypeId)
        {
            TicketType ticket = await context.TicketTypes.AsNoTracking().FirstOrDefaultAsync(t => t.Id == TicketTypeId);
            if (ticket == null)
                return "None";
            return ticket.Name;
        }

  

    }
}
