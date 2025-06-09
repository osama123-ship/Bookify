using Bookify.Extenctions;
using Bookify.Models;
using Bookify.Services.Implementations;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.EventVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookify.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ILogger<EventController> logger;

        public EventController(IEventService eventService, ILogger<EventController> logger)
        {
            this.eventService = eventService;
            this.logger = logger;
        }

        public async Task<IActionResult> DisplayAll()
        {
            logger.LogInformation("DisplayAll called at {Time}", DateTime.Now);
            var eventsVM = await eventService.GetAllAsync();
            return View(eventsVM);
        }

        public async Task<IActionResult> DisplayByCategory(int CategoryId)
        {
            logger.LogInformation("DisplayByCategory called with CategoryId={CategoryId} at {Time}", CategoryId, DateTime.Now);
            var eventsVM = await eventService.GetByCategoryAsync(CategoryId);
            return View(eventsVM);
        }

        public async Task<IActionResult> DetailsForUser(int id)
        {
            logger.LogInformation("DetailsForUser called for EventId={EventId}", id);
            var vm = await eventService.GetByIdAsync(id);
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Add()
        {
            logger.LogInformation("Add GET called");
            ViewBag.Categories = await eventService.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewEventVM newEventVM)
        {
            if (ModelState.IsValid)
            {
                string userId = User.GetUserIdFromClaim();
                logger.LogInformation("Add POST called by UserId={UserId}", userId);

                await eventService.AddAsync(newEventVM, userId);
                return RedirectToAction("DisplayAllForCompany", "Company");
            }
            foreach (var state in ModelState)
                foreach (var error in state.Value.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

            logger.LogWarning("Add POST invalid model state");
            ViewBag.Categories = await eventService.GetAllCategoriesAsync();
            return View(newEventVM);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> Edit(int id)
        {
            logger.LogInformation("Edit called for EventId={EventId}", id);
            var vm = await eventService.GetByIdAsync(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Company")]
        
        public async Task<IActionResult> Edit(EventToViewVM editEventVM)
        {
            logger.LogInformation($"Edit POST called for Event : {editEventVM.Title} ");
            if (ModelState.IsValid) {

                await eventService.UpdateAsync(editEventVM);
                return RedirectToAction("DisplayAllForCompany", "Company");
            }
            logger.LogInformation($"Edit POST invalid model state");
            return View(editEventVM);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Company")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogWarning("Delete GET called for EventId={EventId}", id);
            bool ok = await eventService.DeleteAsync(id);
            if (!ok)
                TempData["DeleteError"] = "Can't delete this event because it's referenced in other records.";

            return RedirectToAction("DisplayAllForCompany", "Company");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisplayForAdmin(int? CategoryId, int? CompanyId, DateTime? Date)
        {
            logger.LogInformation("DisplayForAdmin called with filters: CategoryId={CategoryId}, CompanyId={CompanyId}, Date={Date}",
                CategoryId, CompanyId, Date);
            var events = await eventService.DisplayForAdminAsync(CategoryId, CompanyId, Date);
            return View(events);
        }

    }
}
