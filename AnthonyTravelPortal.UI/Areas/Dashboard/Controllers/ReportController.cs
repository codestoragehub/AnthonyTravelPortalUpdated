using Microsoft.AspNetCore.Mvc;

namespace AnthonyTravelPortal.UI.Areas.Dashboard.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
