namespace Bookify.ViewModels.TemporaryBookingVM
{
    public class TemporaryToViewVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime ReservedAt { get; set; }
        public string UserName { get; set; }
    }
}
