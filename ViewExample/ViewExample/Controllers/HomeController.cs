using Microsoft.AspNetCore.Mvc;
using ViewExample.Models;

namespace ViewExample.Controllers
{
    public class HomeController : Controller
    {
        List<Person> people = new List<Person>()
            {
            new Person()
            {
                PersonName = "Ram",
                DOB = Convert.ToDateTime("2002/12/12"),
                Gender =  "Male"
            },
            new Person()
            {
                PersonName = "Shyam",
                //DOB = Convert.ToDateTime("2000/02/11"),
                Gender =  "Male"
            },
            new Person()
            {
                PersonName = "Sita",
                DOB = Convert.ToDateTime("2001/05/09"),
                Gender =  "Female"
            }
            };
        [Route("home")]
        public IActionResult Index()
        {
            ViewData["title"] = "View Example Rozer views";
            return View("index", people);
        }
        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if (name == null)
                return Content("Name should not null");
            Person matchingPerson = people.Where(tmp => tmp.PersonName == name).FirstOrDefault();
            return View(matchingPerson);
        }
    }
}
