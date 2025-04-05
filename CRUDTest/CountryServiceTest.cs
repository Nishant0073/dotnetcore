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


        // CountryRequestAdd is null then should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullRequest()
        {
            // Arrange
            CountryAddRequest countryRequestAdd = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => countryService.AddCountry(countryRequestAdd));
        }
        // CountryRequestAdd.CountryName is null then should throw ArgumentException
        [Fact]
        public void AddCountry_NullCountryName()
        {
            // Arrange
            CountryAddRequest countryRequestAdd = new CountryAddRequest
            {
                CountryName = null
            };
            // Act & Assert
            Assert.Throws<ArgumentException>(() => countryService.AddCountry(countryRequestAdd));
        }
        // CountryRequestAdd.CountryName is duplicate then should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest countryRequestAdd1 = new CountryAddRequest
            {
                CountryName = "USA"
            };
            CountryAddRequest countryRequestAdd2 = new CountryAddRequest
            {
                CountryName = "USA"
            };
            countryService.AddCountry(countryRequestAdd1);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => countryService.AddCountry(countryRequestAdd2));
        }
        // CountryRequestAdd is valid then it should add the country and return the country response
        [Fact]
        public void AddCountry_ValidRequest()
        {
            // Arrange
            CountryAddRequest countryRequestAdd = new CountryAddRequest
            {
                CountryName = "INDIA"
            };
            // Act
            var response = countryService.AddCountry(countryRequestAdd);
            // Assert
            Assert.NotNull(response);
            Assert.Equal("INDIA", response.CountryName);
            Assert.NotNull(response.CountryId);
        }

    }
}
