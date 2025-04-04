using Microsoft.AspNetCore.Mvc;
using PartialViewExample.Models;

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
        [HttpGet("programming-languages")]
        public IActionResult ProgrammingLanguages()
        {
            ListModel listModel = new ListModel()
            {
                Title = "Programming Languages",
                ListItems = new List<string> {
                    "C++",
                    "C#",
                    "Java",
                    "GO"
                }
            };
            return PartialView("_ListPartialView", listModel);

        }
    }
}

