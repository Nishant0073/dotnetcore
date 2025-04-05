using Entities;

namespace ServiceContracts
{
    /// <summary>
    /// Defines the contract for country-related services.
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds a new country based on the provided request.
        /// </summary>
        /// <param name="countryAddRequest">The request containing the country information to be added.</param>
        /// <returns>A <see cref="CountryResponse"/> object containing the added country's information.</returns>
        CountryResponse AddCountry(CountryAddRequest countryAddRequest);

        /// <summary>
        /// Retrieves all countries.
        /// </summary>
        /// <returns>A list of <see cref="CountryResponse"/> objects containing information about all countries.</returns>
        List<CountryResponse> GetAllCountries();
    }
}
