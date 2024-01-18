using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotManagement.Entities
{
    public class Subscriptions
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [ForeignKey("Subscriber")]
        public int SubscriberId { get; set; }
        public Subscribers? Subscriber { get; set; } //navigation property

        public double DiscountValue { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
  
    }
}
