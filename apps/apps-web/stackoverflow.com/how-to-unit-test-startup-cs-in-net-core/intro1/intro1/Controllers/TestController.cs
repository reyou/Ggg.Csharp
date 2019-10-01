using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intro1.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace intro1.Controllers
{
    public class TestController : Controller
    {
        public TestController(ISomeDependency dependency)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}