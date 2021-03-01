using Microsoft.AspNetCore.Mvc;

namespace intro1.Areas.Products.Controllers
{
    [Area("Products")]
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
