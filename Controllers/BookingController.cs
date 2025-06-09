using Bookify.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
   
   
        [Authorize(Roles = "Admin")]
        public class BookingController : Controller
        {
            private readonly IBookingService _bookingService;

            public BookingController(IBookingService bookingService)
            {
                _bookingService = bookingService;
            }

            public async Task<IActionResult> Index(string? search)
            {
                var bookings = await _bookingService.GetAllBookingsAsync(search);
                ViewBag.Search = search;
                return View(bookings);
            }
        }



    
}
