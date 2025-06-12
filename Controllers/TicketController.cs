using Bookify.Services.Implementations;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TicketVM;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IApplicationUserService userService;

        public TicketController(ITicketService ticketService,IApplicationUserService userService)
        {
            this.ticketService = ticketService;
            this.userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Company")]
        public async Task<IActionResult> ConfirmedTickets(int TicketTypeId, string? UserName)
        {
            List<ConfirmedTicketsVM> tickets = await ticketService.GeTicketsByTicketType(TicketTypeId, UserName);
            ViewBag.TicketTypeId = TicketTypeId;
            ViewBag.TicketTypeName = await ticketService.GetTicketTypeName(TicketTypeId);
            return View(tickets);
        }
        public async Task<IActionResult> ConfirmedTicketsDetails(string UserName,int TicketTypeId)
        {
            string UserId = await userService.GetUserId(UserName);
            List<TicketVM> tickets = await ticketService.GetTicketsByUserIdAsync(UserId, TicketTypeId);
            TempData["UserName"] = UserName;
            TempData["TicketTypeId"] = TicketTypeId;
            return View(tickets);
        }
    }
}
