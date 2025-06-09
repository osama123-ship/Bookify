
using Bookify.Services.Interfaces;
using Bookify.ViewModels.CategoryVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService CategoryService;

        public CategoryController(ICategoryService CategoryService)
        {
            this.CategoryService = CategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await CategoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> New(NewCategoryVM newCategory)
        {
            if (ModelState.IsValid)
            {
                bool check = await CategoryService.AddAsync(newCategory);
                if (check)        
                    return View(new NewCategoryVM());
                ModelState.AddModelError("", "Category is already exists");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await CategoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await CategoryService.UpdateAsync(vm);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await CategoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await CategoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
