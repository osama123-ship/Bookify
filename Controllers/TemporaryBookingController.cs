using Bookify.Extenctions;
using Bookify.Models;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TemporaryBookingVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class TemporaryBookingController : Controller
    {
        protected readonly ITemporaryBookingService temporaryBookingService;
        public TemporaryBookingController(ITemporaryBookingService temporaryBookingService)
        {
            this.temporaryBookingService = temporaryBookingService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TempBooking(int EventId)
        {
            TempData["id"] = EventId;
            string UserId = User.GetUserIdFromClaim();
            ViewBag.CntForUser = await temporaryBookingService.CountTicketsForUser(UserId, EventId);    
            ViewBag.Tickets = await temporaryBookingService.GetTicketsTypeVM(EventId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> TempBooking(BookTemporaryVM bookTemporary)
        {
            int EventId = (int)TempData["id"];
            TempData.Keep("id");
            if (ModelState.IsValid)
            {
                string UserId = User.GetUserIdFromClaim();
                bool check = await temporaryBookingService.BookingTemporary(bookTemporary, UserId, EventId);
                if (check)
                    return RedirectToAction("DetailsForUser", "Event", new { id = EventId });
                ModelState.AddModelError("", "Number of Tickets isn't enough!");
            }
            ViewBag.Tickets = await temporaryBookingService.GetTicketsTypeVM(EventId);
            return View(bookTemporary);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelSingle(int ticketTypeId, int eventId)
        {
            string userId = User.GetUserIdFromClaim();
            await temporaryBookingService.RemoveSingleByUserAsync(userId, eventId, ticketTypeId);
            return RedirectToAction("Cart", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAll()
        {
            string userId = User.GetUserIdFromClaim();
            await temporaryBookingService.RemoveAllByUserIdAsync(userId);
            return RedirectToAction("Cart", "Account");
        }
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> DetailsForTicketType(int EventId, int TicketTypeId, string? UserName)
        {
            var list = await temporaryBookingService.GetAllByEventAndTicketTypeAsync(EventId, TicketTypeId, UserName);
            return View(list);
        }

    }
}
