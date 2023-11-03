using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirQuality.Models
{
    public class AirQualityData
    {
        [Key]
        public int DataID { get; set; }

        [Required]
        [StringLength(50)]
        public string UniqueID { get; set; } // Add this line

        [ForeignKey("AirQualityIndicator")]
        public int IndicatorID { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [Required]
        [StringLength(255)]
        public string TimePeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public double DataValue { get; set; }

        public string Message { get; set; }

        // Navigation properties
        public virtual AirQualityIndicator AirQualityIndicator { get; set; }
        public virtual Location Location { get; set; }
    }
}
