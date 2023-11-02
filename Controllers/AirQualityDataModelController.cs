using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Controllers
{
    public class AirQualityDataModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
