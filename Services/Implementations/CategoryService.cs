using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Implementations;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.CategoryVM;

namespace Bookify.Services.Implementations
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(IBaseRepository<Category> repository, ICategoryRepository categoryRepository) :base(repository) 
        {
           this.categoryRepository = categoryRepository;
        }
        public async Task<bool> AddAsync(NewCategoryVM newCategory)
        {
            Category New= new Category();
            New.Name = newCategory.Name;
            bool Check = await categoryRepository.FindByNameAsync(New);
            if (Check)
                return false;
            
            await categoryRepository.AddAsync(New);
            await categoryRepository.SaveChangesAsync();
            return true;
        }
        public async Task DeleteAsync(int id)
        {
          await categoryRepository.DeleteByIdAsync(id);       
          await categoryRepository.SaveChangesAsync();
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<EditCategoryVM> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null) return new EditCategoryVM();

            return new EditCategoryVM
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> UpdateAsync(EditCategoryVM vm)
        {
            var category = await categoryRepository.GetByIdAsync(vm.Id);
            if (category == null) return false;

            category.Name = vm.Name;
            await categoryRepository.SaveChangesAsync();
            return true;
        }

    }

}
