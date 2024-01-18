namespace ParkingLotManagement.Models
{
    public class PricingPlansForCreationModel
    {
        public int Id { get; set; }
        public decimal HourlyPricing { get; set; }
        public decimal DailyPricing { get; set; }
        public int MinimumHours { get; set; }
        public string Type { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
