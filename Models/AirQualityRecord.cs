using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AirQuality.Models
{
    public class AirQualityRecord
    {
        public override string ToString()
        {
            return $"UniqueID: {UniqueID}, IndicatorID: {IndicatorID}, Name: {Name}, Measure: {Measure}, MeasureInfo: {MeasureInfo}, GeoTypeName: {GeoTypeName}, GeoPlaceName: {GeoPlaceName}, TimePeriod: {TimePeriod}, StartDate: {StartDate}, DataValue: {DataValue}";
        }

        [Key]
        [JsonProperty("unique_id")]
        public string UniqueID { get; set; }

        [JsonProperty("indicator_id")]
        public int IndicatorID { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("measure")]
        public string Measure { get; set; }

        [JsonProperty("measure_info")]
        public string MeasureInfo { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("geo_type_name")]
        public string GeoTypeName { get; set; }

        [JsonProperty("geo_join_id")]
        public string GeoJoinID { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("geo_place_name")]
        public string GeoPlaceName { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("time_period")]
        public string TimePeriod { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("data_value")]
        public double DataValue { get; set; }

        public string Message { get; set; }
    }
}
