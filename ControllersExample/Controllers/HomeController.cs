using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController{
        [Route("/")]
        public string Index(){
            return "Hello from index";
        }

        [Route("/about")]
        public string About(){
            return "Hello from about";
        }
        [Route("/contact-us")]
        public string Contact(){
            return "Hello from contact";
        }
    }
     
}

