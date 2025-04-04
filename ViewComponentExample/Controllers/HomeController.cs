using Microsoft.AspNetCore.Mvc;
using ViewComponentExample.Models;

namespace ViewComponentExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("/friends-list")]
        public IActionResult FriendList()
        {
            PersonGrid grid = new PersonGrid()
            {
                Title = "Employees",
                Persons = new List<Person>()
                {
                    new Person
                    {
                        PersonName = "Shyam",
                        JobTitle = "Manager"
                    },
                    new Person
                    {
                        PersonName = "Ram",
                        JobTitle = "CEO"
                    },
                    new Person
                    {
                        PersonName = "Krishna",
                        JobTitle = "Ass. Manager"
                    },
                }
            };
            return ViewComponent("Grid", new
            {
                grid = grid
            });
        }
    }
}
