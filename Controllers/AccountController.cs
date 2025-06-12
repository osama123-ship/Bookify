using Bookify.Additional;
using Bookify.Extenctions;
using Bookify.Extenctions.Models;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TicketVM;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Bookify.Controllers
{
    public class AccountController : Controller
    {
        protected readonly IApplicationUserService userService;
        string? UserId;
        public AccountController(IApplicationUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                    IdentityResult result = await userService.RegisterAsync(registerVM);
                    if (result.Succeeded)
                    {
                    HttpContext.Session.SetString("User", registerVM.UserName);
                    HttpContext.Session.SetString("email", registerVM.Email);
                    return RedirectToAction("SendAndGetOtpToConfirmAccount");
                    }
                    foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);                
            }
            return View(registerVM);
        }
        [HttpGet]
        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginAsync(loginVM);
                if (result.Succeeded)
                    return RedirectToAction("EventCategories", "Home");
                ModelState.AddModelError("", "Invalid Email/UserName or Password");
            }
            return View(loginVM);
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(UserForgetPasswordVM userForget)
        {
            if (ModelState.IsValid)
            {
                bool Check = await userService.FindByEmailAsync(userForget.Email) != null;
                if (Check)
                {
                    HttpContext.Session.SetString("email", userForget.Email);
                    return RedirectToAction("SendAndGetOtpToResetPassword");
                }
                
                ModelState.AddModelError("", "Wrong Email");
            }
            return View(userForget);
        }
        [HttpGet]
        public IActionResult SendAndGetOtpToResetPassword()
        {
            var email = HttpContext.Session.GetString("email");
            var result = userService.SendAndSaveOtp(email);
            ViewBag.Message = EmailStatusMessages.GetUserMessage(result);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendAndGetOtpToResetPassword(UserOtpVM userOtp)
        {
            if (ModelState.IsValid)
            {
                bool Check = userService.CheckOTP(userOtp);
                if (Check)
                    return RedirectToAction("CreateNewPassword");

                ModelState.AddModelError("", "Wrong or Expired OTP");
            }
            return View(userOtp);
        }

        [HttpGet]
        public IActionResult CreateNewPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewPassword(UserNewPasswordVM userNew)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await userService.CreateNewPassword(userNew);
                if (result.Succeeded)
                    return RedirectToAction("ConfirmResetPassword");
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(userNew);
        }
        [HttpGet]
        public IActionResult SendAndGetOtpToConfirmAccount()
        {
            var email = HttpContext.Session.GetString("email");
            var result = userService.SendAndSaveOtp(email);
            ViewBag.Message = EmailStatusMessages.GetUserMessage(result);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> SendAndGetOtpToConfirmAccount(UserOtpVM userOtp) 
        {
            if (ModelState.IsValid)
            {
                bool Check = userService.CheckOTP(userOtp);
                if (Check)
                {
                    var UserName = HttpContext.Session.GetString("User");
                    if (UserName != null)
                    {
                        await userService.ConfirmEmail(UserName);
                        return RedirectToAction("ConfirmAccount");
                    }
                }
                ModelState.AddModelError("", "Wrong or Expired OTP");
            }
            return View(userOtp);
        }
        public IActionResult ConfirmAccount()
        {
            return View();
        }
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Cart()
        {

            UserId = ClaimsPrincipalExtensions.GetUserIdFromClaim(User);
            List<CartItem> cartItems = await userService.GetCart(UserId);
            HttpContext.Session.SetObject("Cart", cartItems);
            ViewBag.TotalPrice = userService.CalcTotalPriceForCart(cartItems);

            return View(cartItems);
        }
        [Authorize]
        public async Task<IActionResult> CheckOut()
        {
            UserId = User.GetUserIdFromClaim();
            bool Check = await userService.CheckOut(UserId);
            if (Check)
            {
                await userService.MakeBookingProcessAsync(UserId);
                return RedirectToAction("MyTickets");
            }
            ModelState.AddModelError("", "Some of your tickets is Expired Try Again");
            return RedirectToAction("Cart");
        }
        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            UserId = User.GetUserIdFromClaim();

            List<TicketVM> tickets = await userService.GetTicketsByUserIdAsync(UserId, null);
            return View(tickets);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
