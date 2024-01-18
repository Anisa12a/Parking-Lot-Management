namespace ParkingLotManagement.Models
{
    public class SubscribersForCreationModel
    {
        public int SubscriberId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdCardNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
