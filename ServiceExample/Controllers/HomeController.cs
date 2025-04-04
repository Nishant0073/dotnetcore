using CitiesServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace ServiceExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService citiesService;
        public HomeController()
        {
            citiesService = null;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var cities = citiesService.GetCities();
            return View(cities);
        }
    }
}
