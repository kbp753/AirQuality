using Microsoft.AspNetCore.Mvc;
using AirQuality.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AirQuality.Controllers
{
    public class AirQualityOverviewController : Controller
    {
        private readonly AirQualityContext _context;

        public AirQualityOverviewController(AirQualityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAirQualityRecords(int locationId, string indicator)
        {
            // Find all records for the given location and indicator
            var recordsToDelete = _context.AirQualityData
                                          .Where(aqd => aqd.LocationID == locationId && aqd.AirQualityIndicator.Name == indicator);

            // Remove the records
            _context.AirQualityData.RemoveRange(recordsToDelete);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a success response
            return Ok();
        }

        public async Task<IActionResult> GetAirQualityRecords(int page = 1, string cityFilter = "", int pageSize = 20)
        {
            // Start by querying locations
            var locationQuery = _context.Locations
                                        .AsQueryable();

            if (!string.IsNullOrEmpty(cityFilter))
            {
                locationQuery = locationQuery.Where(loc => loc.GeoPlaceName.Contains(cityFilter));
            }

            // Get all locations
            var locations = await locationQuery.ToListAsync();

            // Construct the records with associated indicators for each location
            var records = new List<object>();
            foreach (var location in locations)
            {
                var indicators = await _context.AirQualityData
                                                .Where(aqd => aqd.LocationID == location.LocationID)
                                                .Select(aqd => aqd.AirQualityIndicator.Name)
                                                .Distinct()
                                                .ToListAsync();

                foreach (var indicator in indicators)
                {
                    records.Add(new
                    {
                        LocationId = location.LocationID,
                        LocationName = location.GeoPlaceName,
                        LastRecordedQuality = indicator
                    });
                }
            }

            // Apply pagination
            var paginatedRecords = records
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            // Calculate total pages
            var totalCount = records.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Return the data along with pagination information
            return Json(new
            {
                Items = paginatedRecords,
                CurrentPage = page,
                TotalPages = totalPages
            });
        }

    }
}
