using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ReceivedDataModel
{
    [JsonProperty("name")]
    [Required]
    public string Name { get; set; }

    [JsonProperty("measure")]
    [Required]
    public string Measure { get; set; }

    [JsonProperty("measure_info")]
    public string MeasureInfo { get; set; }

    [JsonProperty("geo_type_name")]
    [Required]
    public string GeoTypeName { get; set; }

    [JsonProperty("geo_place_name")]
    [Required]
    public string GeoPlaceName { get; set; }

    [JsonProperty("time_period")]
    [Required]
    public string TimePeriod { get; set; }

    [JsonProperty("start_date")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime StartDate { get; set; }

    [JsonProperty("data_value")]
    [Required]
    public double DataValue { get; set; }
}
