using intro1.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace intro1.Controllers
{
    public class Test2Controller : Controller
    {
        private IMemberService _memberService;
        private IPreferencesService _preferencesService;

        public  Test2Controller(IMemberService memberService, IPreferencesService preferencesService)
        {
            _memberService = memberService;
            _preferencesService = preferencesService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}