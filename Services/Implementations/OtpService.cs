using Bookify.Services.Interfaces;

namespace Bookify.Services.Implementations
{
    public static class OtpService 
    {
        public static string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
