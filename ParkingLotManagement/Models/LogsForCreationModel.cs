namespace ParkingLotManagement.Models
{
    public class LogsForCreationModel
    {
        public string Code { get; set; } = string.Empty;
        public int? SubscriptionId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string FirstName { get; set; } = string.Empty;
    }
}
