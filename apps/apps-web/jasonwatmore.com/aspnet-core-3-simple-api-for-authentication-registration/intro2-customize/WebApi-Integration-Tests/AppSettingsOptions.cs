using Microsoft.Extensions.Options;
using WebApi.Helpers;

namespace WebApi_Integration_Tests
{
    public class AppSettingsOptions : IOptions<AppSettings>
    {
        public AppSettings Value { get; }

        public AppSettingsOptions()
        {
            Value = new AppSettings()
            {
                Secret = "ArgumentOutOfRangeException"
            };
        }
    }
}