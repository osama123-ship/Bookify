using Bookify.Models;
using Bookify.ViewModels.EventVM;

namespace Bookify.Services.Interfaces
{
    public interface IEventService:IBaseService<Event>
    {
        Task<List<DisplayEventsVM>> GetAllAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task AddAsync(NewEventVM newEvent, string UserId);
        Task<EventToViewVM> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<List<DisplayEventsVM>> GetByCategoryAsync(int CategoryId);
        Task<List<DisplayForAdminVM>> DisplayForAdminAsync();
        Task UpdateAsync(EventToViewVM @event);
    }
}
