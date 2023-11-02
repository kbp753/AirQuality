using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Controllers
{
    public class AirQualityAboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
