using Microsoft.AspNetCore.Mvc;

namespace LayoutExample.Controllers
{
    public class ProductController : Controller
    {
        [Route("/products")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/search-products/{product_id}")]
        public IActionResult Search(int product_id)
        {
            ViewBag.productId = product_id;
            return View();
        }
        [Route("/order-products")]
        public IActionResult Order()
        {
            return View();
        }
    }
}
