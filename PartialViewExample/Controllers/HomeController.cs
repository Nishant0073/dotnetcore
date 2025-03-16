using Microsoft.AspNetCore.Mvc;

namespace PartialViewExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Contries";
            ViewData["ListItems"] = new List<string>()
            {
                "India",
                "Russia",
                "Israel",
                "UAE",
                "China"
            };

         return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}

