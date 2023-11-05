using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AirQuality.Models; // Import your models namespace if it's different

namespace AirQuality.Controllers
{
    public class AirQualityDetailsController : Controller
    {
        private readonly AirQualityContext _context;

        public AirQualityDetailsController(AirQualityContext context)
        {
            _context = context;
        }

        [HttpGet("/AirQualityDetails/Index")]
        public async Task<IActionResult> Index(int locationId, string indicator)
        {
            // Fetch the location name
            var location = await _context.Locations
                                         .Where(l => l.LocationID == locationId)
                                         .FirstOrDefaultAsync();

            if (location == null)
            {
                // Handle the case where the location is not found
                return NotFound();
            }

            var measure = await _context.AirQualityIndicators
                                         .Where(i => i.Name == indicator)
                                         .FirstOrDefaultAsync();
            if (measure == null)
            {
                return NotFound();
            }

            // Fetch the records
            var records = await _context.AirQualityData
                            .Where(aqd => aqd.LocationID == locationId && aqd.AirQualityIndicator.Name == indicator)
                            .OrderByDescending(aqd => aqd.StartDate) // Order by StartDate descending
                            .ToListAsync();


            // Construct the ViewModel to pass to the view
            var viewModel = new AirQualityDetailsViewModel
            {
                LocationName = location.GeoPlaceName,
                MeasureName = measure.Measure,
                Records = records,
                IndicatorName=indicator
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }

        [HttpDelete("/AirQualityDetails/Delete")]
        public async Task<IActionResult> Delete(int dataId)
        {
            var record = await _context.AirQualityData.FindAsync(dataId);
            if (record == null)
            {
                return NotFound();
            }

            _context.AirQualityData.Remove(record);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
