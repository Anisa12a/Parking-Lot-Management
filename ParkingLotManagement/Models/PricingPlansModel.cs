namespace ParkingLotManagement.Models
{
    public class PricingPlansModel
    {
        public int Id { get; set; }
        public decimal HourlyPricing { get; set; }
        public decimal DailyPricing { get; set; }
        public int MinimumHours { get; set; }
        public string Type { get; set; } = string.Empty; // "weekday" or "weekend" and this is a non-nullable property
        public bool IsDeleted { get; set; }
    }
}
