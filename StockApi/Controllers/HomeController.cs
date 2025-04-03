using Microsoft.AspNetCore.Mvc;

namespace StockApi.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
