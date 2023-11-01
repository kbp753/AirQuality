using System.ComponentModel.DataAnnotations;

namespace AirQuality.Models
{
    public class AirQualityRecord
    {
        [Key]
        public string UniqueID { get; set; }

        public int IndicatorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Measure { get; set; }

        public string MeasureInfo { get; set; }

        [Required]
        [StringLength(255)]
        public string GeoTypeName { get; set; }

        public string GeoJoinID { get; set; }

        [Required]
        [StringLength(255)]
        public string GeoPlaceName { get; set; }

        [Required]
        [StringLength(255)]
        public string TimePeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public double DataValue { get; set; }

        public string Message { get; set; }

    }
}
