using Bookify.Models;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TicketTypeVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin,Company")]

    public class TicketTypeController : Controller
    {
        private readonly ITicketTypeService ticketTypeService;
        public TicketTypeController(ITicketTypeService ticketTypeService)
        {
         this.ticketTypeService = ticketTypeService;
        }

        // No Page
        [HttpGet]
        public IActionResult New(int EventId)
        {
            TempData["id"] = EventId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewTicketTypeVM newTicketType)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["id"];
                TempData.Keep("id");
                await ticketTypeService.AddAsync(newTicketType, id);
                return RedirectToAction("Edit", "Event", new { id = id });
            }
            return View(newTicketType);
        }
        [HttpGet]
        public async Task<IActionResult> TicketTypeDetailsForAdmin(int EventId)
        {
            List<DetailsForAdminVM> Tickets = await ticketTypeService.TicketTypeDetailsForAdmin(EventId);
            TempData["EventId"] = EventId;
            return View(Tickets);
        }
    }
}