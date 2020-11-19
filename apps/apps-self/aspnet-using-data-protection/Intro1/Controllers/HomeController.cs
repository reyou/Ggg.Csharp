using Intro1.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text;

namespace Intro1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDataProtectionProvider _provider;
        private IDataProtector _protector;


        public HomeController(ILogger<HomeController> logger, IDataProtectionProvider provider)
        {
            _logger = logger;
            _provider = provider;
            _protector = provider.CreateProtector("Contoso.MyClass.v1");

        }

        public IActionResult Index()
        {
            // protect the payload
            byte[] input = Encoding.ASCII.GetBytes("this is string to protect!"); ;
            byte[] protectedPayload = _protector.Protect(input);
            string str = Encoding.ASCII.GetString(protectedPayload);
            Console.WriteLine($"Protect returned: {str}");

            // unprotect the payload
            byte[] unprotectedPayload = _protector.Unprotect(protectedPayload);
            string str2 = Encoding.ASCII.GetString(unprotectedPayload);
            Console.WriteLine($"Unprotect returned: {str2}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
