using Bookify.Extenctions.Models;
using Bookify.Models;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using static Bookify.Services.Implementations.EmailService;

namespace Bookify.Services.Interfaces
{
    public interface IApplicationUserService:IBaseService<ApplicationUser>
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<IdentityResult> RegisterAsync(UserRegisterVM _user);
        Task<SignInResult> LoginAsync(UserLoginVM _user);
        Task<IdentityResult> CreateNewPassword(UserNewPasswordVM userPassword);
        Task LogoutAsync();
        Task ConfirmEmail(string UserName);
        EmailSendStatus SendAndSaveOtp(string email);
        bool CheckOTP(UserOtpVM user);
        (string,DateTime) GetOTP();
        Task<List<CartItem>> GetCart(string UserId);
        long CalcTotalPriceForCart(List<CartItem> cartItems);
        Task<bool> CheckOut(string UserId);
        Task MakeBookingProcessAsync(string UserId);

    }
}
