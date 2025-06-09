using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.ViewModels.CategoryVM;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context context) : base(context)
        {
        }
        public async Task<bool> FindByNameAsync(Category newCategory)
        {
            Category New = new Category { Name = newCategory.Name };
            return await context.Categories.FirstOrDefaultAsync(obj => obj.Name == newCategory.Name) != null;
        }
    }
}
