using ServiceContracts;
using Entities;
using System.Runtime.CompilerServices;
namespace Services
{
    /// <summary>
    /// Provides services related to country operations.
    /// </summary>
    public class CountryService : ICountryService
    {
        private List<Country> _countries = new List<Country>();

        #region Add Country

        /// <summary>
        /// Adds a new country based on the provided request.
        /// </summary>
        /// <param name="countryAddRequest">The request containing the country information to be added.</param>
        /// <returns>A <see cref="CountryResponse"/> object containing the added country's information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="countryAddRequest"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the country name is null or the country already exists.</exception>
        public CountryResponse AddCountry(CountryAddRequest countryAddRequest)
        {
            // request if null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            if (_countries.Where(tmp => tmp.CountryName == countryAddRequest.CountryName).Any())
            {
                throw new ArgumentException("Country already exists");
            }

            // request is valid
            Country country = new Country
            {
                CountryName = countryAddRequest.CountryName,
                CountryId = Guid.NewGuid(),
            };

            _countries.Add(country);
            return country.ToCountryResponse();
        }
        #endregion

        #region GetAllCountries
        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }
        #endregion
    }
}
