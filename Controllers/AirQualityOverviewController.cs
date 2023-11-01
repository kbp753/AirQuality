using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Controllers
{
    public class AirQualityOverviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
