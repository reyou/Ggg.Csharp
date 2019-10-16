using System.Collections.Generic;

namespace intro1
{
    public interface ICountryService
    {
        IEnumerable<Country> All();
    }
}