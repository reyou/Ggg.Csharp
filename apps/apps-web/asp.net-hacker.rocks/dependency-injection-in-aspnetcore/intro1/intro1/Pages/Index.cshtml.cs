using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace intro1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private ICountryService _countryService;
        public IndexModel(ILogger<IndexModel> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public void OnGet()
        {

        }
    }
}
