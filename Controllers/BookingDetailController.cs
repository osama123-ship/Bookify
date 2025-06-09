using Bookify.ViewModels.TicketVM;
using Bookify.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookify.Services.Interfaces;

namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookingDetailController : Controller
    {
        private readonly IBookingDetailService bookingDetailService;

        public BookingDetailController(IBookingDetailService bookingDetailService)
        {
            this.bookingDetailService = bookingDetailService;
        }

        public async Task<IActionResult> Index(int BookingId)
        {
            var bookingDetails = await bookingDetailService.GetBookingDetails(BookingId);
            return View(bookingDetails);
        }

    }
}
