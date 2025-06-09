using System.Security.Claims;
using static Bookify.Services.Implementations.EmailService;

namespace Bookify.Additional
{
    public static class EmailStatusMessages
    {
        public static string GetUserMessage(EmailSendStatus status)
        {
            return status switch
            {
                EmailSendStatus.Success => "Your verification code has been sent successfully.",
                EmailSendStatus.InvalidEmailFormat => "The email address format is invalid. Please check and try again.",
                EmailSendStatus.RecipientNotFound => "The recipient email address was not found.",
                EmailSendStatus.SmtpServerError => "A server error occurred while sending the email. Please try again later.",
                EmailSendStatus.AuthenticationFailed => "Failed to authenticate the sender email. Please contact support.",
                EmailSendStatus.NetworkError => "Network connection error. Please check your internet connection.",
                _ => "An unexpected error occurred while sending the email. Please try again."
            };
        }
    }    

}
