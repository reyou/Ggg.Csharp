using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intro1.Areas.Products.Controllers
{
    public class HomeController : Controller
    {
        [Area("Products")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
