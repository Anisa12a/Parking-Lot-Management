using ParkingLotManagement.Models;

namespace ParkingLotManagement
{
    //in-memory data for ParkingSpots functionality:
    public class ParkingDataStore
    {
        public List<ParkingSpotsModel> ParkingSpots { get; set; }
        // public static ParkingDataStore Current { get; } = new ParkingDataStore(); //ky line sipas tutorialit chapter 5 lesson 10 min 5 behet comment me vone
        public ParkingDataStore()
        {
            //init dummy data
            ParkingSpots = new List<ParkingSpotsModel>()
            {
                new ParkingSpotsModel()
                {
                    Id = 1,
                    ReservedSpots = 10,
                    RegularSpots = 20
                },
            };
        }

    }
    
    //in-memory data for PricingPlans functionality:
    public class ParkingDataStore1 
    { 
        public List<PricingPlansModel> PricingPlans { get; set; }
       // public static ParkingDataStore1 Current { get; } = new ParkingDataStore1();
        public ParkingDataStore1()
        {
            PricingPlans = new List<PricingPlansModel>()
            {
                new PricingPlansModel()
                {
                    Id = 1,
                    HourlyPricing = 200,
                    DailyPricing = 600,
                    MinimumHours = 1,
                    Type = "weekday",
                },
            };
        }
    }

    //in-memory data for Subscribers functionality:
    public class ParkingDataStore2
    {
        public List<SubscribersModel> Subscribers { get; set; }

        //public static ParkingDataStore2 Current { get; } = new ParkingDataStore2();
        public ParkingDataStore2()
        {
            Subscribers = new List<SubscribersModel>()
            {
                new SubscribersModel()
                {
                    SubscriberId = 10,
                    FirstName = "Ann",
                    LastName = "Larking",
                    IdCardNumber = "J62415347S",
                    Email = "ann_larking@yahoo.com",
                    PhoneNumber = "825673",
                    Birthday = new DateTime(1966-05-30),
                    PlateNumber = "5E",
                    IsDeleted = false,
                },
                 new SubscribersModel()
                {
                    SubscriberId = 20,
                    FirstName = "Elly",
                    LastName = "Allen",
                    IdCardNumber = "K24579102L",
                    Email = "elly_allen@yahoo.com",
                    PhoneNumber = "907346",
                    Birthday = new DateTime(1971-11-15),
                    PlateNumber = "3G",
                    IsDeleted = false,
                },
            };
        }

    }

    //in-memory data for Subscriptions functionality:
    public class ParkingDataStore3
    {
        public List<SubscriptionsModel> Subscriptions { get; set; }
        //public static ParkingDataStore3 Current { get; } = new ParkingDataStore3();
        public ParkingDataStore3()
        {
            Subscriptions = new List<SubscriptionsModel>()
            {
                new SubscriptionsModel()
                {
                SubscriptionId = 1001,
                Code = "06E",
                SubscriberId = 10,
                DiscountValue = 20,
                StartDate = new DateTime(2023-08-02),
                EndDate = new DateTime(2023-08-31),
                IsDeleted = false
                },
                new SubscriptionsModel()
                {
                SubscriptionId = 1002,
                Code = "08G",
                SubscriberId = 20,
                DiscountValue = 50,
                StartDate = new DateTime(2023-09-01),
                EndDate = new DateTime(2023-09-30),
                IsDeleted = false
                },
            };
        }
    }

        //in-memory data for Logs functionality:
        public class ParkingDataStore4
        {
            public List<LogsModel> Logs { get; set; }
            //public static ParkingDataStore4 Current { get; } = new ParkingDataStore4();
            public ParkingDataStore4()
            {
                Logs = new List<LogsModel>()
                {
                    new LogsModel()
                    {
                        LogId = 1,
                        Code = "12A",
                        SubscriptionId = 1001,
                        CheckInTime = new DateTime(2023, 08, 06, 10, 28, 06),
                        CheckOutTime = new DateTime(2023, 08, 06, 15, 00, 04),
                        FirstName = "Ann",
                        IsDeleted = false
                    },
                    new LogsModel()
                    {
                        LogId = 2,
                        Code = "02L",
                        SubscriptionId = 1002,
                        CheckInTime = new DateTime(2023, 08, 02, 11, 15, 01),
                        CheckOutTime = new DateTime(2023, 08, 02, 18, 28, 06),
                        FirstName = "Elly",
                        IsDeleted = false
                    }
                };
            }
        }
}
