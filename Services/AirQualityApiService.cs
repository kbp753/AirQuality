using AirQuality.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Buffers.Text;
using System.Reflection.Metadata;
using System;

namespace AirQuality.Services
{
    public class AirQualityApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AirQualityApiService> _logger;

        public AirQualityApiService(HttpClient httpClient, IServiceProvider serviceProvider, ILogger<AirQualityApiService> logger)
        {
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<string> GetAirQualityDataAsync(string apiUrl)
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task FetchAndUpdateData()
        {
            _logger.LogInformation("Fetching data from the API.");
            var response = await _httpClient.GetAsync("https://data.cityofnewyork.us/resource/c3uy-2p5r.json");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var records = JsonConvert.DeserializeObject<List<AirQualityRecord>>(content);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AirQualityContext>();
                    foreach (var record in records)
                    {
                        // Check if the data record already exists
                        var existingData = await _context.AirQualityData
                            .FirstOrDefaultAsync(aqd => aqd.UniqueID == record.UniqueID);
                        if (existingData == null)
                        {
                            _logger.LogInformation($"Inserting new record with UniqueID: {record.UniqueID}");

                            // Check if the location already exists
                            var location = await _context.Locations
                                .FirstOrDefaultAsync(l => l.GeoType == record.GeoTypeName && l.GeoPlaceName == record.GeoPlaceName);
                            if (location == null && !string.IsNullOrEmpty(record.GeoPlaceName))
                            {
                                location = new Location
                                {
                                    GeoType = record.GeoTypeName,
                                    GeoPlaceName = record.GeoPlaceName
                                };
                                _context.Locations.Add(location);
                                await _context.SaveChangesAsync(); // Save to get a LocationID
                            }
                            else if (location == null)
                            {
                                // Handle the case where GeoPlaceName is null
                                // For example, log a warning or assign a default value
                                _logger.LogWarning($"GeoPlaceName is null for record with UniqueID: {record.UniqueID}");
                                // Continue to the next record or assign a default value
                                continue;
                            }

                            // Check if the indicator already exists
                            var airQualityIndicator = await _context.AirQualityIndicators
                                .FirstOrDefaultAsync(aqi => aqi.Name == record.Name && aqi.Measure == record.Measure);
                            if (airQualityIndicator == null)
                            {
                                airQualityIndicator = new AirQualityIndicator
                                {
                                    Name = record.Name,
                                    Measure = record.Measure,
                                    MeasureInfo = record.MeasureInfo
                                };
                                _context.AirQualityIndicators.Add(airQualityIndicator);
                                await _context.SaveChangesAsync(); // Save to get an IndicatorID
                            }

                            // Create new AirQualityData record
                            var airQualityData = new AirQualityData
                            {
                                UniqueID = record.UniqueID,
                                TimePeriod = record.TimePeriod,
                                StartDate = record.StartDate,
                                DataValue = record.DataValue,
                                Message = record.Message ?? "No message provided",
                                LocationID = location.LocationID,
                                IndicatorID = airQualityIndicator.IndicatorID
                            };
                            _context.AirQualityData.Add(airQualityData);
                        }
                        // If the record already exists, you can update it here if needed
                        // else
                        // {
                        //     existingData.TimePeriod = record.TimePeriod;
                        //     existingData.StartDate = record.StartDate;
                        //     existingData.DataValue = record.DataValue;
                        //     existingData.Message = record.Message;
                        //     // No need to update LocationID and IndicatorID as they should be the same
                        // }

                        // Save changes inside the using scope block
                        await _context.SaveChangesAsync();
                    }
                    _logger.LogInformation($"Record raw data: {records[0]}");
                }
            }
        }

    }
}