using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Controllers
{
    public class AirQualityDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
