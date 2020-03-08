using Microsoft.Extensions.Localization;
using System.Globalization;

namespace intro1.Models
{
    public class HomeIndexViewModel
    {
        public CultureInfo[] CultureInfos { get; set; }
        public LocalizedString HelloMessage { get; set; }
    }
}
