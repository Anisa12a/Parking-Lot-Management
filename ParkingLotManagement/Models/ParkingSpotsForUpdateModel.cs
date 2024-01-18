namespace ParkingLotManagement.Models
{
    public class ParkingSpotsForUpdateModel
    {
        public int Id { get; set; }
        public int ReservedSpots { get; set; }
        public int RegularSpots { get; set; }
        public bool IsDeleted { get; set; }
    }
}
