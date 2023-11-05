using System.Collections.Generic;
using AirQuality.Models;

public class AirQualityDetailsViewModel
{
    public string LocationName { get; set; }
    public string MeasureName { get; set; }
    public List<AirQualityData> Records { get; set; }

    public string IndicatorName { get; set; }

}
