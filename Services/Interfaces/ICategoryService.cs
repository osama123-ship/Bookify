using Bookify.Models;
using Bookify.ViewModels.CategoryVM;

namespace Bookify.Services.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task<bool> AddAsync(NewCategoryVM newCategory);
        Task<List<CategoryVM>> GetAllAsync();
        Task<EditCategoryVM> GetByIdAsync(int id);
        Task<bool> UpdateAsync(EditCategoryVM vm);
        Task DeleteAsync(int id);
    }
}
