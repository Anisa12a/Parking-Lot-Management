using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotManagement.Entities
{
    public class PricingPlans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public decimal HourlyPricing { get; set; }

        [Required]
        public decimal DailyPricing { get; set; }

        [Required]
        public int MinimumHours { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        /* public PricingPlans(string type) //we added a constructor for non-nullable property "Type"
        {
            Type = type;
        } */
        public bool IsDeleted { get; set; }
    } 
}
