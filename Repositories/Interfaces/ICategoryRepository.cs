using Bookify.Models;
using Bookify.ViewModels.CategoryVM;

namespace Bookify.Repositories.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        Task<bool> FindByNameAsync(Category newCategory);
    }
}
