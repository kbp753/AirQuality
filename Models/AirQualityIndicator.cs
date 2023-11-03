using System.ComponentModel.DataAnnotations;

namespace AirQuality.Models
{
    public class AirQualityIndicator
    {
        public virtual ICollection<AirQualityData> AirQualityData { get; set; }

        public AirQualityIndicator()
        {
            AirQualityData = new HashSet<AirQualityData>();
        }
        [Key]
        public int IndicatorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Measure { get; set; }

        public string MeasureInfo { get; set; }
    }
}
