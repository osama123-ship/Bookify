using Bookify.Models;
using Bookify.Repositories.Implementations;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.EventVM;
using Bookify.ViewModels.TicketTypeVM;
using Bookify.Services.Interfaces;

namespace Bookify.Services.Implementations
{
    public class EventService :BaseService<Event>, IEventService
    {
        protected readonly IEventRepository eventRepository;
        protected readonly ITemporaryBookingRepository temporaryBookingRepository;
        public EventService(IEventRepository eventRepository, ITemporaryBookingRepository temporaryBookingRepository,
            IBaseRepository<Event> repository) : base(repository)
        {
            this.eventRepository = eventRepository;
            this.temporaryBookingRepository = temporaryBookingRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await eventRepository.GetAllCategoriesAsync();
        }
        public async Task AddAsync(NewEventVM newEvent, string UserId)
        {
            int CompanyId = await eventRepository.GetCompanyIdByUserIdAsync(UserId);
            Event @event = new Event(newEvent.Title, newEvent.Description, newEvent.StartBookingTime, newEvent.EndBookingTime,
                newEvent.StartTime, newEvent.EndTime, newEvent.Location, newEvent.CategoryId, CompanyId);
            await eventRepository.AddAsync(@event);
            await eventRepository.SaveChangesAsync();
        }
        public async Task<EventToViewVM> GetByIdAsync(int id)
        {
            Event @event = await eventRepository.GetByIdAsync(id);

            EventToViewVM vm = new EventToViewVM
            {
                Id = @event.Id,
                Title = @event.Title,
                CategoryId = @event.CategoryId,
                CompanyId = @event.CompanyId,
                Description = @event.Description,
                StartBookingTime = @event.StartBookingTime,
                EndBookingTime = @event.EndBookingTime,
                StartTime = @event.StartTime,
                EndTime = @event.EndTime,
                Location = @event.Location
            };
            return vm;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await eventRepository.GetByIdAsync(id);
            if (obj.Sales != 0)
            {
                return false;
            }
            await eventRepository.DeleteByIdAsync(id);
            await eventRepository.SaveChangesAsync();
            return true;
        }
        public async Task<List<DisplayEventsVM>> GetAllAsync()
        {
            List<Event> events = (List<Event>)await eventRepository.GetAllAsync();
            List<DisplayEventsVM> eventsVM = new List<DisplayEventsVM>();
            if (events == null)
                return eventsVM;
            foreach (Event e in events)
            {
                eventsVM.Add(new DisplayEventsVM
                { Id = e.Id, EndDate = e.EndTime, StartDate = e.StartTime, Title = e.Title });
            }
            return eventsVM;
        }
        public async Task<List<DisplayEventsVM>> GetByCategoryAsync(int CategoryId)
        {
            List<Event> events = await eventRepository.GetByCategoryIdAsync(CategoryId);
            List<DisplayEventsVM> eventsVM = new List<DisplayEventsVM>();
            if (events == null)
                return eventsVM;
            foreach (Event e in events)
            {
                if (e.TicketTypes.Count == 0)
                    continue;
                eventsVM.Add(new DisplayEventsVM
                { Id = e.Id, EndDate = e.EndTime, StartDate = e.StartTime, Title = e.Title });
            }
            return eventsVM;
        }
        public async Task<List<DisplayForAdminVM>> DisplayForAdminAsync()
        {
            var Events = await eventRepository.GetAllWithCategoryAsync();
            List<DisplayForAdminVM> Show = new List<DisplayForAdminVM>();
            foreach (Event e in Events)
            {
                Show.Add(new DisplayForAdminVM
                {
                    CategoryId = e.Id,
                    CategoryName = e.Category.Name,
                    EventId = e.Id,
                    EventName = e.Title,
                    Sales = e.Sales
                });
            }
            return Show;
        }
        public async Task UpdateAsync(EventToViewVM @event)
        {
            Event Event = await eventRepository.GetByIdAsync(@event.Id);

            Event.Id = @event.Id;
              Event.Title = @event.Title;
            Event.Description = @event.Description;
            Event.StartBookingTime = @event.StartBookingTime;
            Event.EndBookingTime = @event.EndBookingTime;
            Event.StartTime = @event.StartTime;
            Event.EndTime = @event.EndTime;
            Event.Location = @event.Location;
            await eventRepository.SaveChangesAsync();
        }
    }
}
