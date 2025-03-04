using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class HelloController : Controller
    {
        // GET: HelloController
        public ActionResult Index()
        {
            return View();
        }

    }
}
