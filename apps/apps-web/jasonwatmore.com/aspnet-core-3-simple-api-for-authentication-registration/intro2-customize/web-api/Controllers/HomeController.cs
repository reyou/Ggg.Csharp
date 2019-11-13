using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}