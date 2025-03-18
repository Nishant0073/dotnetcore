﻿using Microsoft.AspNetCore.Mvc;

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
    }
}
