using System.ComponentModel.DataAnnotations;

namespace AirQuality.Models
{
    public class Location
    {
        public virtual ICollection<AirQualityData> AirQualityData { get; set; }
        public Location()
        {
            AirQualityData = new HashSet<AirQualityData>();
        }
        [Key]
        public int LocationID { get; set; }

        [Required]
        [StringLength(255)]
        public string GeoType { get; set; }

        [Required]
        [StringLength(255)]
        public string GeoPlaceName { get; set; }
    }
}
