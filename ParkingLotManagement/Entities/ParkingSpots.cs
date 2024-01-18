using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotManagement.Entities
{
    public class ParkingSpots
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public int ReservedSpots { get; set; }

        [Required]
        public int RegularSpots { get; set; }
        public bool IsDeleted { get; set; }
    }
}
