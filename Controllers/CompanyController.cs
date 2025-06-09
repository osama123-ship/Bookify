using Bookify.Extenctions;
using Bookify.Models;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.CompanyVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Authorize(Roles = Access)]
    public class CompanyController : Controller
    {
        private const string Access = "Company,Admin";
        private readonly ICompanyService CompanyService;
        public CompanyController(ICompanyService CompanyService)
        {
            this.CompanyService = CompanyService;
        }
        public async Task<IActionResult> DisplayAllForCompany(int? CategoryId, DateTime? Date)
        {
            string UserID = User.GetUserIdFromClaim();
            int CompanyId = await CompanyService.GetCompanyIdByUserIdAsync(UserID);
            var events = await CompanyService.DisplayForAdminAsync(CategoryId, CompanyId, Date);
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string userId = User.GetUserIdFromClaim();
            var model = await CompanyService.GetCompanyProfileAsync(userId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCompanyProfileVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await CompanyService.UpdateCompanyProfileAsync(model);
            TempData["Success"] = "Done";
            return RedirectToAction(nameof(DisplayAllForCompany));
        }
    }
}
