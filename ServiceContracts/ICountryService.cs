using Entities;

namespace ServiceContracts
{
    /// <summary>
    /// Defines the contract for country-related services.
    /// </summary>
    public interface ICountryService
    {
        // Define methods for the service contract here
        public CountryResponse AddCountry(CountryAddRequest countryAddRequest);
    }
}
