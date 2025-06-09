using Bookify.Services.Interfaces;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index(string? search, string? role)
        {
            var effectiveRole = string.IsNullOrEmpty(role) || role == "None" ? null : role;

            var users = await userService.GetUsersAsync(search, effectiveRole);
            var roles = await userService.GetRolesAsync();

            ViewBag.Roles = roles.Prepend("None").ToList();
            ViewBag.SelectedRole = role ?? "All Roles";
            ViewBag.Search = search ?? string.Empty;

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            ViewBag.Roles = await userService.GetRolesAsync();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserVM userVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await userService.GetRolesAsync();
                return View(userVm);
            }

            await userService.UpdateUserAsync(userVm);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
