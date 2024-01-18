using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.Entities;

namespace ParkingLotManagement.DbContexts
{
    public class ParkingInfoContext : DbContext
    {
        public DbSet<ParkingSpots> ParkingSpots { get; set; }
        public DbSet<PricingPlans> PricingPlans { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Logs> Logs { get; set; }

        public ParkingInfoContext(DbContextOptions<ParkingInfoContext> options) : base(options)
        {
        }
    } 
}
