using Microsoft.AspNetCore.Mvc;
using System;

namespace intro1.Controllers
{
    public class StatusCodeObjectsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OkObjectResult()
        {
            var result = new OkObjectResult(new { message = "200 OK", currentDate = DateTime.Now });
            return result;
        }

        [HttpGet]
        public IActionResult CreatedObjectResult()
        {
            var result = new CreatedAtActionResult("createdobjectresult", "statuscodeobjects", "", new { message = "201 Created", currentDate = DateTime.Now });
            return result;
        }

        [HttpGet]
        public IActionResult BadRequestObjectResult()
        {
            var result = new BadRequestObjectResult(new { message = "400 Bad Request", currentDate = DateTime.Now });
            return result;
        }

        [HttpGet]
        public IActionResult NotFoundObjectResult()
        {
            //This is a test
            var result = new NotFoundObjectResult(new { message = "404 Not Found", currentDate = DateTime.Now });
            return result;
        }

        [HttpGet]
        public IActionResult ObjectResult(int statusCode)
        {
            var result = new ObjectResult(new { statusCode = statusCode, currentDate = DateTime.Now });
            result.StatusCode = statusCode;
            return result;
        }
    }
}