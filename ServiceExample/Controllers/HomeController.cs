using Microsoft.AspNetCore.Mvc;
using CitiesService;

namespace ServiceExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyCitiesService citiesService;
        public HomeController()
        {
            citiesService = new MyCitiesService();
        }

        [Route("/")]
        public IActionResult Index()
        {
            var cities = citiesService.GetCities();
            return View(cities);
        }
    }
}
