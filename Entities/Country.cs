namespace Entities
{
    /// <summary>
    /// Represents a country with an ID and a name.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the unique identifier for the country.
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string? CountryName { get; set; }
    }
}
