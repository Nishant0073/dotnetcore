using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using Services;

namespace CRUDTest
{
    public class CountryServiceTest
    {
        ICountryService countryService;
        public CountryServiceTest()
        {
            countryService = new CountryService();
        }

    }
}
