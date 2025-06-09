using Bookify.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Bookify.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public enum EmailSendStatus
        {
            Success,
            InvalidEmailFormat,
            RecipientNotFound,
            SmtpServerError,
            AuthenticationFailed,
            NetworkError,
            UnknownError
        }

        public EmailSendStatus SendOtpEmail(string toEmail, string otp)
        {
            string subject = "(OTP)";
            string body = $"<p>Your OTP code is : <strong>{otp}</strong></p><p>Available for 5 minutes only</p>";

            string? fromEmail = _config["EmailSettings:From"];
            string? appPassword = _config["EmailSettings:AppPassword"];

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, appPassword),
                EnableSsl = true,
            };

            try
            {
                var message = new MailMessage(fromEmail, toEmail, subject, body)
                {
                    IsBodyHtml = true
                };

                smtpClient.Send(message);
                return EmailSendStatus.Success;
            }
            catch (FormatException)
            {
                return EmailSendStatus.InvalidEmailFormat;
            }
            catch (SmtpFailedRecipientException)
            {
                return EmailSendStatus.RecipientNotFound;
            }
            catch (SmtpException smtpEx)
            {
                if (smtpEx.Message.Contains("authentication", StringComparison.OrdinalIgnoreCase))
                    return EmailSendStatus.AuthenticationFailed;

                return EmailSendStatus.SmtpServerError;
            }
            catch (System.Net.WebException)
            {
                return EmailSendStatus.NetworkError;
            }
            catch
            {
                return EmailSendStatus.UnknownError;
            }
        }
    }
}
