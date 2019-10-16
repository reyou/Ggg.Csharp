using System.Collections.Generic;

namespace intro1
{
    public class CountryService : ICountryService
    {
        public CountryService()
        {

        }
        public IEnumerable<Country> All()
        {
            return new List<Country>
            {
                new Country {Code = "DE", Name = "Germany" },
                new Country {Code = "FR", Name = "France" },
                new Country {Code = "CH", Name = "Switzerland" },
                new Country {Code = "IT", Name = "Italy" },
                new Country {Code = "DK", Name = "Danmark" } ,
                new Country {Code = "US", Name = "United States" }
            };
        }
    }
}
