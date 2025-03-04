using Microsoft.AspNetCore.Mvc;
using ControllersExample.Models;
namespace ControllersExample.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/")]
        public string Index()
        {
            return "Hello from index";
        }

        [Route("/about")]
        public string About()
        {
            return "Hello from about";
        }
        [Route("/contact-us")]
        public ContentResult Contact()
        {
            // return new ContentResult{
            //     Content = "Hello from contacts",
            //     ContentType = "text/plain"
            // };

            return Content("<h1> Welcome to contact</h1> \b <h2>Hello from contacts</h2>", "text/html");
        }


        [Route("/person")]
        public JsonResult Person()
        {
            var person = new Person{
                Id = Guid.NewGuid(),
                FirstName = "Nishant",
                LastName = "Shingate",
                Age = 22
            };
            //return new JsonResult(person);
            return Json(person);
        }
    }

}

