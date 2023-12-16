using Microsoft.AspNetCore.Mvc;

namespace CodeAnswers.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
