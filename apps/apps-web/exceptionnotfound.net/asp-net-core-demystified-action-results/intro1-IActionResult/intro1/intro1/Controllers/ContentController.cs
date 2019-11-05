using Microsoft.AspNetCore.Mvc;
using System;

namespace intro1.Controllers
{
    public class ContentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PartialViewResult()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult JsonResult()
        {
            return Json(new
            {
                message = "This is a JSON result.",
                date = DateTime.Now
            });
        }

        [HttpGet]
        public IActionResult ContentResult()
        {
            return Content("Here's the ContentResult message.");
        }
    }
}