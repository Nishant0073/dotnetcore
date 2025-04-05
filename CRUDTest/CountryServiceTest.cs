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
    /// <summary>
    /// Contains unit tests for the <see cref="CountryService"/> class.
    /// </summary>
    public class CountryServiceTest
    {
        private readonly ICountryService countryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryServiceTest"/> class.
        /// </summary>
        public CountryServiceTest()
        {
            countryService = new CountryService();
        }

        #region AddCountry

        /// <summary>
        /// Tests that <see cref="CountryService.AddCountry(CountryAddRequest)"/> throws <see cref="ArgumentNullException"/> when the request is null.
        /// </summary>
        [Fact]
        public void AddCountry_NullRequest()
        {
            // Arrange
            CountryAddRequest countryRequestAdd = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => countryService.AddCountry(countryRequestAdd));
        }

        /// <summary>
        /// Tests that <see cref="CountryService.AddCountry(CountryAddRequest)"/> throws <see cref="ArgumentException"/> when the country name is null.
        /// </summary>
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

        /// <summary>
        /// Tests that <see cref="CountryService.AddCountry(CountryAddRequest)"/> throws <see cref="ArgumentException"/> when the country name is a duplicate.
        /// </summary>
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

        /// <summary>
        /// Tests that <see cref="CountryService.AddCountry(CountryAddRequest)"/> adds a valid country and returns the expected response.
        /// </summary>
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

        /// <summary>
        /// Tests that <see cref="CountryService.GetAllCountries"/> returns an empty list when no countries are added.
        /// </summary>
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            // Act
            var response = countryService.GetAllCountries();

            // Assert
            Assert.Empty(response);
        }

        /// <summary>
        /// Tests that <see cref="CountryService.GetAllCountries"/> returns the correct list of countries after adding countries.
        /// </summary>
        [Fact]
        public void GetCountries_AddCounties()
        {
            // Arrange
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

            List<CountryResponse> countries_list_from_add_countries = new List<CountryResponse>();

            foreach (var country in countries_request_list)
            {
                countries_list_from_add_countries.Add(countryService.AddCountry(country));
            }

            // Act
            var actual_getAllCountries_response = countryService.GetAllCountries();

            // Assert
            foreach (var country in countries_list_from_add_countries)
            {
                Assert.Contains(country, actual_getAllCountries_response);
            }
        }

        #endregion

        #region GetCountryById

        /// <summary>
        /// Tests that <see cref="CountryService.GetCountryById(Guid?)"/> returns null when the country ID is null.
        /// </summary>
        [Fact]
        public void GetCountryById_Null()
        {
            // Act
            CountryResponse response = countryService.GetCountryById(null);

            // Assert
            Assert.Null(response);
        }

        /// <summary>
        /// Tests that <see cref="CountryService.GetCountryById(Guid?)"/> returns the correct country when a valid ID is provided.
        /// </summary>
        [Fact]
        public void GetCountryById_ValidId()
        {
            // Arrange
            CountryAddRequest countryRequestAdd = new CountryAddRequest
            {
                CountryName = "INDIA"
            };
            // Add the country to the service
            var response = countryService.AddCountry(countryRequestAdd);

            // Act
            var actual_getCountryById_response = countryService.GetCountryById(response.CountryId);

            // Assert
            Assert.Equal(response.CountryName, actual_getCountryById_response.CountryName);
        }

        #endregion
    }
}
