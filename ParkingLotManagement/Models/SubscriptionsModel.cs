namespace ParkingLotManagement.Models
{
    public class SubscriptionsModel
    {
        public int SubscriptionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int SubscriberId { get; set; }
        public double DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }

        public decimal Price(PricingPlansModel pricingPlan)
        {
            // We calculate the number of days between StartDate and EndDate:
            int totalDays = (int)(EndDate - StartDate).TotalDays;

            // We calculate the total price without discount:
            decimal totalPrice = totalDays * pricingPlan.HourlyPricing;

            // We apply the discount value:
            totalPrice -= (totalPrice * (decimal)(DiscountValue / 100));

            return totalPrice;
        }
    }
}

/* namespace ParkingLotManagement.Models
{
    public class SubscriptionsModel
    {
        public int SubscriptionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int SubscriberId { get; set; }
        public double DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }

        public PricingPlansModel PricingPlan { get; set; }

        public decimal CalculatedPrice // Computed property
        {
            get
            {
                // We calculate the number of days between StartDate and EndDate:
                int totalDays = (int)(EndDate - StartDate).TotalDays;

                // We calculate the total price without discount:
                decimal totalPrice = totalDays * PricingPlan.HourlyPricing;

                // We apply the discount value:
                totalPrice -= (totalPrice * (decimal)(DiscountValue / 100));

                return totalPrice;
            }
        }
    }
} */



