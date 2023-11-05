namespace AirQuality.Models
{
    public class AirQualityOverviewViewModel
    {
        public string GeoPlaceName { get; set; }
        public List<IndicatorData> Indicators { get; set; }

        public class IndicatorData
        {
            public string Name { get; set; }
            public string LastRecordedValue { get; set; }
        }
    }

}
