using Bookify.Extenctions;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.RolesVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService) {
            this.roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleService.GetAllAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewRoleVM newRole)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleService.AddAsync(newRole);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                
                foreach (var error in result.Errors)     
                    ModelState.AddModelError("", error.Description);           
            }
            return View(newRole);
        }
        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            var roles = await roleService.GetAllAsync();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(RoleToUserVM roleToUser)
        {
            var roles = await roleService.GetAllAsync();
            ViewBag.Roles = roles;
            if (ModelState.IsValid)
            {
                bool check = await roleService.AddRoleToUserAsync(roleToUser);
                if (check)
                {
                    await roleService.CheckIfCompany(roleToUser);
                    return View(new RoleToUserVM());
                }
                ModelState.AddModelError("", "UserName is not found!");
            }
            return View(roleToUser);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleService.GetByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(new EditRoleVM { Id = role.Id, Name = role.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                var result = await roleService.UpdateAsync(roleVM);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(roleVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleService.GetByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await roleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
