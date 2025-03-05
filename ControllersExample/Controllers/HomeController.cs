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
            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Nishant",
                LastName = "Shingate",
                Age = 22
            };
            //return new JsonResult(person);
            return Json(person);
        }
        [Route("/file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("dia1.pdf", "application/pdf");
            return File("dia1.pdf", "application/pdf");
        }

        [Route("/file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult("/home/nishant/dia1.pdf", "application/pdf");
            return PhysicalFile("/home/nishant/dia1.pdf", "application/pdf");
        }
        [Route("/file-download3")]
        public FileContentResult FileDownload3()
        {
            Byte[] bytes = System.IO.File.ReadAllBytes("/home/nishant/Pictures/Screenshots/image1.png");
            return File(bytes,"application/img");
        }
    }

}

