using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers
{
    public class StationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
