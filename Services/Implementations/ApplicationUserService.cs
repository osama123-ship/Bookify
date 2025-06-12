using Bookify.Extenctions;
using Bookify.Extenctions.Models;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.TicketVM;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using static Bookify.Services.Implementations.EmailService;
 
namespace Bookify.Services.Implementations
{
    public class ApplicationUserService : BaseService<ApplicationUser>, IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContext;
        private readonly ITemporaryBookingRepository temporaryRepository;
        private readonly ITicketTypeRepository ticketTypeRepository;

        public ApplicationUserService(IBaseRepository<ApplicationUser> repository,UserManager<ApplicationUser> userManager, 
         SignInManager<ApplicationUser> signInManager, IEmailService emailService, IHttpContextAccessor httpContext,
         ITemporaryBookingRepository temporaryRepository, ITicketTypeRepository ticketTypeRepository
         ) : base(repository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.httpContext = httpContext;
            this.temporaryRepository = temporaryRepository;
            this.ticketTypeRepository = ticketTypeRepository;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterVM _user)
        {
            var user = new ApplicationUser
            {
                UserName = _user.UserName,
                Email = _user.Email,
                PhoneNumber = _user.PhoneNumber,
                Address = _user.Address
            };
            IdentityResult result = await userManager.CreateAsync(user, _user.Password);
            if (result.Succeeded)
                user.EmailConfirmed = false;
            return result;
        }
        public async Task ConfirmEmail(string UserName)
        {
            var user = await userManager.FindByNameAsync(UserName);
            user.EmailConfirmed = true;
            await userManager.AddToRoleAsync(user, "User");
        }
        public async Task<SignInResult> LoginAsync(UserLoginVM _user)
        {
            var userEmail = await userManager.FindByEmailAsync(_user.EmailOrUserName);
            var userName = await userManager.FindByNameAsync(_user.EmailOrUserName);
            ApplicationUser user = null;
            if (userEmail != null)
                user = userEmail;
            if (userName != null)
                user = userName;
            if (user != null)
            {
                bool check = await userManager.CheckPasswordAsync(user, _user.Password);
                if (check)
                {
                    await signInManager.SignInAsync(user, _user.RememberMe);
                    return SignInResult.Success;
                }

            }
            return SignInResult.Failed;
        }
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateNewPassword(UserNewPasswordVM userPassword)
        {
            string Email = httpContext.HttpContext.Session.GetString("Email");
            ApplicationUser user = await FindByEmailAsync(Email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            return await userManager.ResetPasswordAsync(user, token, userPassword.Password);
        }

        public (string, DateTime) GetOTP()
        {
            var http = httpContext.HttpContext;
            var storedOtp = http.Session.GetString("OTP");
            var expiresAt = DateTime.Parse(http.Session.GetString("OTP_ExpiresAt") ?? DateTime.MinValue.ToString());

            return (storedOtp, expiresAt);
        }
        public EmailSendStatus SendAndSaveOtp(string email)
        {
            string otp = OtpService.GenerateOtp();
            var result = emailService.SendOtpEmail(email, otp);
            httpContext.HttpContext.Session.SetString("Email", email);
            httpContext.HttpContext.Session.SetString("OTP", otp);
            httpContext.HttpContext.Session.SetString("OTP_ExpiresAt", DateTime.Now.AddMinutes(2).ToString());
            return result;
        }
        public bool CheckOTP(UserOtpVM user)
        {
            var (otp, otp_expire) = GetOTP();
            return otp == user.otp && otp_expire >= DateTime.Now;
        }
        public async Task<List<CartItem>> GetCart(string UserId)
        {
            List<CartItem> cartItems = temporaryRepository.GetByUserId(UserId);
            foreach (var item in cartItems)
            {
                var Type = await ticketTypeRepository.GetByIdAsync(item.TicketTypeId);
                item.EventId = Type.EventId;
                item.PricePerTicket = Type.Price;
            }
            return cartItems;
        }
        public long CalcTotalPriceForCart(List<CartItem> cartItems)
        {
            long totalPrice = 0;
            foreach (var cartItem in cartItems)
                totalPrice += (long)cartItem.PricePerTicket * cartItem.Quantity;
            return totalPrice;
        }
        public async Task<bool> CheckOut(string UserId)
        {
            List<CartItem> cartItems = httpContext.HttpContext.Session.
                GetObject<List<CartItem>>("Cart") ?? new List<CartItem>(),
                cartItemsCheck = temporaryRepository.GetByUserId(UserId);
            foreach (var item in cartItemsCheck)
            {
                var Type = await ticketTypeRepository.GetByIdAsync(item.TicketTypeId);
                item.EventId = Type.EventId;
                item.PricePerTicket = Type.Price;
            }
            bool Check = cartItems.SequenceEqual(cartItemsCheck);
            if (!Check)
                return false;
            List<TemporaryBooking> Temps = new List<TemporaryBooking>();
            foreach (var cartItem in cartItems)
            {
                var temp = await temporaryRepository.GetByEventIdAndTicketTypeIdAsync(cartItem.EventId, cartItem.TicketTypeId);
                var TicketType = await ticketTypeRepository.GetByIdAsync(temp.TicketTypeId);
                TicketType.ConfirmedTickets += cartItem.Quantity;
                Temps.Add(temp);
            }
            await temporaryRepository.Remove(Temps.AsQueryable());
            return true;
        }
        public async Task MakeBookingProcessAsync(string UserId)
        {
            List<CartItem> cartItems = httpContext.HttpContext.Session.
            GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            await MakeBookingProcessAsync(UserId, cartItems);
        }
        public async Task<string> GetUserId(string UserName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(UserName);
            if (user == null)
                return string.Empty;
            return user.Id;
        }
    }
}
