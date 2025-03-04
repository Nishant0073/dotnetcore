using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/")]
        public string Index(){
            return "Hello from index";
        }

        [Route("/about")]
        public string About(){
            return "Hello from about";
        }
        [Route("/contact-us")]
        public ContentResult Contact(){
            // return new ContentResult{
            //     Content = "Hello from contacts",
            //     ContentType = "text/plain"
            // };

            return Content("<h1> Welcome to contact</h1> \b <h2>Hello from contacts</h2>","text/html");
        }
    }
     
}

