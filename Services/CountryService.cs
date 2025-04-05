using ServiceContracts;
using Entities;
using System.Runtime.CompilerServices;
namespace Services
{
    public class CountryService : ICountryService
    {
        private List<Country> _countries = new List<Country>();
        public CountryResponse AddCountry(CountryAddRequest countryAddRequest)
        {
            // request if null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            if(_countries.Where(tmp => tmp.CountryName == countryAddRequest.CountryName).Any())
            {
                throw new ArgumentException("Country already exists");
            }

            //request is valid
            Country country = new Country
            {
                CountryName = countryAddRequest.CountryName,
                CountryId = Guid.NewGuid(),
            };

            _countries.Add(country);
            return country.ToCountryResponse();

        }
    }
}
