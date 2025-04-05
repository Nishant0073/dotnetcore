using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        #region AddCountry

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
            var _get_allcountries_response = countryService.GetAllCountries();

            // Assert
            Assert.NotNull(response);
            Assert.Equal("INDIA", response.CountryName);
            Assert.Contains(response, _get_allcountries_response);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Act
            var response = countryService.GetAllCountries();
            //Assert
            Assert.Empty(response);
        }

        [Fact]
        public void GetCountries_AddCounties()
        {
            //Arrange
            List<CountryAddRequest> countries_request_list = new List<CountryAddRequest>
            {
                new CountryAddRequest
                {
                    CountryName = "RUSSIA"
                },
                new CountryAddRequest
                {
                    CountryName = "INDIA"
                },
                new CountryAddRequest
                {
                    CountryName = "ISREL"
                }
            };


            List<CountryResponse>  countries_list_from_add_countries = new List<CountryResponse>();

            foreach (var country in countries_request_list)
            {
                countries_list_from_add_countries.Add(countryService.AddCountry(country));
            }

            //Act
            var actual_getAllCountries_response = countryService.GetAllCountries();

            //Assert
            foreach (var country in countries_list_from_add_countries)
            {
                Assert.Contains(country,actual_getAllCountries_response);
            }
            
        }
        #endregion

    }
}
