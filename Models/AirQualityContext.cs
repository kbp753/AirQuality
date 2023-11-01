using Microsoft.EntityFrameworkCore;

namespace AirQuality.Models
{
    public class AirQualityContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AirQualityContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

}
