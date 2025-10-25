using Microsoft.AspNetCore.Mvc;

namespace AdHoc_SpeechSynthesizer.Controllers
{
    public class MessageTemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
