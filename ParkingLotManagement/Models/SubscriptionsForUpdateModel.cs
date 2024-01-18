namespace ParkingLotManagement.Models
{
    public class SubscriptionsForUpdateModel
    {
        public int SubscriptionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int SubscriberId { get; set; }
        public decimal Price { get; set; }
        public double DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
