using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Controllers
{
    public class AirQualityHealthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
