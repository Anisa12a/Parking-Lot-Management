namespace ParkingLotManagement.Models
{
    public class ParkingSpotsModel
    {
        public int Id { get; set; }
        public int ReservedSpots { get; set; }
        public int RegularSpots { get; set; }
        public int TotalSpots //computed property
        {
            get { return ReservedSpots + RegularSpots; }
        }
        public bool IsDeleted { get; set; } //I have added this property to perform soft delete
    }
}
