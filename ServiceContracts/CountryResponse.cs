using Entities;

namespace ServiceContracts
{
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
    }
    public static class CountryExtensions
    {
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
