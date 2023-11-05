namespace AirQuality.Models
{
    public class AirQualityDataEdit
    {
        public int DataID { get; set; }
        public string Name { get; set; }

        public string UniqueID { get; set; }

        public int IndicatorID { get; set; }

        public int LocationID { get; set; }
        public string Measure { get; set; }
        public string MeasureInfo { get; set; }
        public string GeoTypeName { get; set; }
        public string GeoPlaceName { get; set; }
        public string TimePeriod { get; set; }
        public DateTime StartDate { get; set; }
        public double DataValue { get; set; }
        // Add other properties as needed
    }

}
