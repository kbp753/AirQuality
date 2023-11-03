using Microsoft.EntityFrameworkCore;

namespace AirQuality.Models
{
    public class AirQualityContext : DbContext
    {
        public AirQualityContext(DbContextOptions<AirQualityContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<AirQualityIndicator> AirQualityIndicators { get; set; }
        public DbSet<AirQualityData> AirQualityData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and any additional configurations here
            modelBuilder.Entity<AirQualityData>()
                .HasOne(aqd => aqd.Location)
                .WithMany(loc => loc.AirQualityData)
                .HasForeignKey(aqd => aqd.LocationID);

            modelBuilder.Entity<AirQualityData>()
                .HasOne(aqd => aqd.AirQualityIndicator)
                .WithMany(aqi => aqi.AirQualityData)
                .HasForeignKey(aqd => aqd.IndicatorID);
        }
    }

}

