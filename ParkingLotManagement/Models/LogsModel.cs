namespace ParkingLotManagement.Models
{
    public class LogsModel
    {
        public int LogId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int? SubscriptionId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        // We add this property to store the subscriber's first name:
        public string FirstName { get; set; } = string.Empty;

        //We also add a IsDeleted property in order to be able to perform a soft delete method:
        public bool IsDeleted { get; set; }

        //Property to store the calculated price:
        public decimal CalculatePrice(PricingPlansModel pricingPlan)
        {
            if (SubscriptionId.HasValue)
            {
                return 0;
            }
            else
            {
                if (CheckOutTime == null)
                {
                    throw new InvalidOperationException("CheckOutTime cannot be null.");
                }

                //1: We check if the check-in has happened on a weekday or weekend:
                bool isWeekend = (CheckInTime.DayOfWeek == DayOfWeek.Saturday) || (CheckInTime.DayOfWeek == DayOfWeek.Sunday);
                bool isWeekdayPlan = pricingPlan.Type == "weekday";

                //2: We calculate the total number of hours the car has spent in the parking lot:
                TimeSpan totalParkingTime = CheckOutTime.Value - CheckInTime;
                double totalHours = totalParkingTime.TotalHours;

                //3: We check the minimum hours of the payment plan:
                int minimumHours = pricingPlan.MinimumHours;

                //4/1: If the car has spent less or equal to the minimum hours, we calculate the price based on hourly rate:
                if (totalHours <= minimumHours)
                {
                    decimal totalPrice = (decimal)totalHours * pricingPlan.HourlyPricing;
                    return totalPrice;
                }
                else
                {
                //4/2: If the car has spent more than the minimum hours, we calculate the price based on daily rate:
                    int totalDays = (int)Math.Floor(totalHours / 24);
                    double remainingHours = totalHours % 24;

                //4/2/1: If remaining hours are less than the minimum hours, we calculate the price based on hourly rate:
                    if (remainingHours <= minimumHours)
                    {
                        decimal hourlyPrice = (decimal)remainingHours * pricingPlan.HourlyPricing;
                        decimal dailyPrice = (decimal)totalDays * pricingPlan.DailyPricing;
                        decimal totalPrice = hourlyPrice + dailyPrice;
                        return totalPrice;
                    }
                    else
                    {   
                //4/2/2: If there are remaining hours greater than the minimum hours, we add one more day to the full number of days:
                        totalDays++;
                        decimal totalPrice = (decimal)totalDays * pricingPlan.DailyPricing;
                        return totalPrice;
                    }
                }
            }
        }
    }
}

