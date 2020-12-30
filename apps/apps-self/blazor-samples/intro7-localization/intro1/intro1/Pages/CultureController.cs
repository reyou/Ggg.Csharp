using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;

namespace intro1.Pages
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
            }
            return LocalRedirect(redirectUri);
        }
    }
}
