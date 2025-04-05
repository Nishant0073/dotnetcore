using Entities;

namespace ServiceContracts
{
    /// <summary>
    /// Represents a response containing country information.
    /// </summary>
    public class CountryResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for the country.
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string CountryName { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            if(obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }

            CountryResponse countryResponse = (CountryResponse)obj;

            return CountryId == countryResponse.CountryId &&
                   CountryName == countryResponse.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    /// <summary>
    /// Provides extension methods for converting <see cref="Country"/> objects to <see cref="CountryResponse"/> objects.
    /// </summary>
    public static class CountryExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Country"/> object to a <see cref="CountryResponse"/> object.
        /// </summary>
        /// <param name="country">The country to convert.</param>
        /// <returns>A <see cref="CountryResponse"/> object containing the country information.</returns>
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName ?? string.Empty
            };
        }

    }

}
