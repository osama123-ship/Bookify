using Bookify.Models;
using Bookify.ViewModels.EventVM;

namespace Bookify.Repositories.Interfaces
{
    public interface IEventRepository:IBaseRepository<Event>
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Event>> GetByCategoryIdAsync(int id);
        Task<List<Event>> GetAllWithCategoryAsync();
    }
}