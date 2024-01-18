using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotManagement.Entities
{
    public class Logs
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;
        // Foreign key for the Subscription
        public int? SubscriptionId { get; set; }

        // Navigation property for the Subscription
        [ForeignKey("SubscriptionId")]
        public Subscriptions? Subscription { get; set; }

        [Required]
        public DateTime CheckInTime { get; set; }

        [Required]
        public DateTime? CheckOutTime { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        /*We add constructor for non-nullable properties:
        public Logs(string code, string firstName)
        {
            Code = code;
            FirstName = firstName; 
        } */
    }
}
