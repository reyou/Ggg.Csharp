using intro1.Models;
using intro1.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;

namespace intro1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStringLocalizer<SharedResource> _localizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _logger = logger;
            _localizer = sharedLocalizer;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
            homeIndexViewModel.CultureInfos = cultureInfos;
            homeIndexViewModel.HelloMessage = _localizer["Hello"];
            return View(homeIndexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
