using static Bookify.Services.Implementations.EmailService;

namespace Bookify.Services.Interfaces
{
    public interface IEmailService
    {
        EmailSendStatus SendOtpEmail(string toEmail, string otp);
    }
}