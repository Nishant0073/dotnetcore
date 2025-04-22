using Entities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ServiceContracts.DTOs;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using ServiceContracts.Enums;

namespace CRUDTest
{
    /// <summary>
    /// Contains unit tests for the <see cref="PersonService"/> class.
    /// </summary>
    public class PersonServiceTest
    {
        private readonly PersonService _personService;
        private readonly CountryService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonServiceTest"/> class.
        /// </summary>
        /// <param name="testOutputHelper">The test output helper for logging test output.</param>
        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();
            _countryService = new CountryService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson

        /// <summary>
        /// Tests that <see cref="PersonService.AddPerson(PersonAddRequest)"/> throws <see cref="ArgumentNullException"/> when the request is null.
        /// </summary>
        [Fact]
        public void AddPerson_NullRequest()
        {
            // Arrange
            PersonAddRequest personRequestAdd = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personRequestAdd));
        }

        /// <summary>
        /// Tests that <see cref="PersonService.AddPerson(PersonAddRequest)"/> throws <see cref="ArgumentException"/> when the person name is null.
        /// </summary>
        [Fact]
        public void AddPerson_NullPersonName()
        {
            // Arrange
            PersonAddRequest personRequestAdd = new PersonAddRequest
            {
                PersonName = null
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _personService.AddPerson(personRequestAdd));
        }

        /// <summary>
        /// Tests that <see cref="PersonService.AddPerson(PersonAddRequest)"/> adds a valid person and returns the expected response.
        /// </summary>
        [Fact]
        public void AddPerson_ValidRequest()
        {
            // Arrange
            PersonAddRequest personRequestAdd = new PersonAddRequest
            {
                PersonName = "John Doe",
                Email = "john@mail.com",
                DateOfBirth = new DateTime(2002, 1, 1),
                Gender = GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Main St",
                RecieveNewsLetter = true
            };

            // Act 
            var response_from_add_person = _personService.AddPerson(personRequestAdd);
            var response_from_getAllPerson = _personService.GetAllPersons();

            // Assert
            Assert.True(response_from_add_person.PersonId != Guid.Empty);
            Assert.Contains(response_from_add_person, response_from_getAllPerson);
        }

        #endregion

        #region GetPersonById

        /// <summary>
        /// Tests that <see cref="PersonService.GetPersonById(Guid?)"/> returns null when the person ID is null.
        /// </summary>
        [Fact]
        public void GetPersonById_NullPersonId()
        {
            // Arrange & Act
            PersonResponse? personResponse_from_GetPersonById = _personService.GetPersonById(null);

            // Assert
            Assert.Null(personResponse_from_GetPersonById);
        }

        /// <summary>
        /// Tests that <see cref="PersonService.GetPersonById(Guid?)"/> returns the correct person when a valid ID is provided.
        /// </summary>
        [Fact]
        public void GetPersonById_ValidPersonId()
        {
            // Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest
            {
                CountryName = "SPAIN"
            };

            CountryResponse country_from_AddCountry = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john@mail.com",
                DateOfBirth = new DateTime(2002, 1, 1),
                Gender = GenderOptions.Male,
                CountryId = country_from_AddCountry.CountryId,
                Address = "123 Main St",
                RecieveNewsLetter = true
            };

            PersonResponse personResponse_from_AddPerson = _personService.AddPerson(personAddRequest);

            // Act
            PersonResponse personResponse_from_GetPersonById = _personService.GetPersonById(personResponse_from_AddPerson.PersonId);

            // Assert
            Assert.Equal(personResponse_from_AddPerson.PersonId, personResponse_from_GetPersonById.PersonId);
        }

        #endregion

        #region GetAllPersons

        /// <summary>
        /// Tests that <see cref="PersonService.GetAllPersons"/> returns an empty list when no persons are added.
        /// </summary>
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            // Arrange & Act
            var persons = _personService.GetAllPersons();

            // Assert
            Assert.Empty(persons);
        }

        /// <summary>
        /// Tests that <see cref="PersonService.GetAllPersons"/> returns the correct list of persons after adding persons.
        /// </summary>
        [Fact]
        public void GetAllPerson_validRequest()
        {
            // Arrange

            List<PersonResponse> personResponses_from_AddPerson = GetAddedPersonsToAddPersons();

            _testOutputHelper.WriteLine("Person Responses from AddPerson:Expected::");
            foreach (PersonResponse personResponse in personResponses_from_AddPerson)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            // Act
            List<PersonResponse> personResponses_from_GetAllPersons = _personService.GetAllPersons();

            _testOutputHelper.WriteLine("Person Responses from GetAllPersons:Actual::");
            foreach (PersonResponse personResponse in personResponses_from_GetAllPersons)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            // Assert
            foreach (var person in personResponses_from_AddPerson)
            {
                Assert.Contains(person, personResponses_from_GetAllPersons);
            }
        }

        #endregion

        #region GetFilteredPersons

        /// <summary>
        /// Tests that <see cref="PersonService.GetFilteredPersons(string?, string?)"/> returns all persons when no filter is applied.
        /// </summary>
        [Fact]
        public void GetFilterdPerson_AllPersons()
        {
            // Arrange
            List<PersonResponse> personResponses_from_AddPerson = GetAddedPersonsToAddPersons();

            // Act
            var filteredPersons = _personService.GetFilteredPersons("", "");
            // Assert
            foreach (var person in personResponses_from_AddPerson)
            {
                Assert.Contains(person, filteredPersons);
            }
        }

        /// <summary>
        /// Tests that <see cref="PersonService.GetFilteredPersons(string?, string?)"/> returns the correct persons when filtered by name.
        /// </summary>
        [Fact]
        public void GetFilterdPerson_ByName()
        {
            // Arrange
            List<PersonResponse> personResponses_from_AddPerson = GetAddedPersonsToAddPersons();
            // Act
            List<PersonResponse> filteredPersons = _personService.GetFilteredPersons(nameof(Person.PersonName), "th");
            // Assert

            foreach (PersonResponse person in personResponses_from_AddPerson)
            {
                if (person.PersonName.Contains("th"))
                {
                    Assert.Contains(person, filteredPersons);
                }
            }
        }
        #endregion


        #region GetSortedPersons
        /// <summary>
        /// Get Sorted Person by their property in ascending order or descending order.
        /// </summary>
        [Fact]
        public void GetSortedPerson()
        {
            // Arrange
            List<PersonResponse> personResponses_from_AddPerson = GetAddedPersonsToAddPersons();
            // Act
            List<PersonResponse> sortedPersons = _personService.GetSortedPerson(personResponses_from_AddPerson, nameof(Person.PersonName), SortOrderEnum.DSC);

            personResponses_from_AddPerson = personResponses_from_AddPerson.OrderByDescending(x => x.PersonName).ToList();

            _testOutputHelper.WriteLine("Sorted Persons:Expected::");
            foreach (PersonResponse personResponse in personResponses_from_AddPerson)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }

            _testOutputHelper.WriteLine("Sorted Persons:Actual::");
            foreach (PersonResponse personResponse in sortedPersons)
            {
                _testOutputHelper.WriteLine(personResponse.ToString());
            }
            // Assert
            for (int i = 0; i < sortedPersons.Count; i++)
            {
                Assert.Equal(sortedPersons[i], personResponses_from_AddPerson[i]);
            }
        }
        #endregion

        #region UpdatePerson
        [Fact]
        public void UpdatePerson_NullRequest()
        {
            //Arrange
            PersonUpdateRequest personUpdateRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });

        }

        [Fact]
        public void UpdatePerson_NullName()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "INDIA"
            };
            CountryResponse country = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Alice Smith",
                Email = "alice.smith@example.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = GenderOptions.Female,
                CountryId = country.CountryId,
                Address = "456 Elm St",
                RecieveNewsLetter = true
            };

            PersonResponse personResponse_from_AddPerson = _personService.AddPerson(personAddRequest);
            PersonUpdateRequest personUpdateRequest = personResponse_from_AddPerson.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });

        }

        [Fact]
        public void UpdatePerson_InvalidPersonId()
        {
            //Arrange
            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = Guid.NewGuid(),
            };

            //Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.UpdatePerson(personUpdateRequest);
            });

        }


        [Fact]
        public void UpdatePerson_ValidRequest()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "INDIA"
            };
            CountryResponse country = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Alice Smith",
                Email = "alice.smith@example.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = GenderOptions.Female,
                CountryId = country.CountryId,
                Address = "456 Elm St",
                RecieveNewsLetter = true
            };

            PersonResponse personResponse_from_AddPerson = _personService.AddPerson(personAddRequest);
            PersonUpdateRequest personUpdateRequest = personResponse_from_AddPerson.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = "Nishant";
            personUpdateRequest.Email = "nishant@example.com";
            personUpdateRequest.DateOfBirth = new DateTime(1995, 1, 1);

            //Act
            PersonResponse personResponse_from_updatePerson = _personService.UpdatePerson(personUpdateRequest);
            PersonResponse personResponse_from_GetPersonById = _personService.GetPersonById(personResponse_from_updatePerson.PersonId);

            //Assert
            Assert.Equal(personResponse_from_updatePerson, personResponse_from_GetPersonById);



        }
        #endregion

        #region DeletePerson
        [Fact]
        public void DeletePerson_NullId()
        {
            //Arrange
            Guid? personId = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.DeletePerson(personId);
            });
        }

        [Fact]
        public void DeletePerson_InvalidId()
        {
            //Arrange
            Guid personId = Guid.NewGuid();

            //Act
            bool isDeleted = _personService.DeletePerson(personId);

            //Assert
            Assert.False(isDeleted);
        }

        public void DeletePerson_ValidPerson()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "INDIA"
            };
            CountryResponse country = _countryService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Alice Smith",
                Email = "alice.smith@example.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = GenderOptions.Female,
                CountryId = country.CountryId,
                Address = "456 Elm St",
                RecieveNewsLetter = true
            };

            PersonResponse personResponse_from_AddPerson = _personService.AddPerson(personAddRequest);

            //Act
            bool isDeleted = _personService.DeletePerson(personResponse_from_AddPerson.PersonId);
            
            //Assert
            Assert.True(isDeleted);

        }
        #endregion
        #region Utility Methods

        /// <summary>
        /// Adds multiple persons to the service and returns the added persons.
        /// </summary>
        /// <returns>A list of <see cref="PersonResponse"/> objects representing the added persons.</returns>
        public List<PersonResponse> GetAddedPersonsToAddPersons()
        {
            // Arrange
            CountryAddRequest countryAddRequest1 = new CountryAddRequest
            {
                CountryName = "USA"
            };

            CountryAddRequest countryAddRequest2 = new CountryAddRequest
            {
                CountryName = "Canada"
            };

            CountryResponse country_from_AddCountry1 = _countryService.AddCountry(countryAddRequest1);
            CountryResponse country_from_AddCountry2 = _countryService.AddCountry(countryAddRequest2);

            List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>
                    {
                        new PersonAddRequest
                        {
                            PersonName = "Alice Smith",
                            Email = "alice.smith@example.com",
                            DateOfBirth = new DateTime(1990, 5, 15),
                            Gender = GenderOptions.Female,
                            CountryId = country_from_AddCountry1.CountryId,
                            Address = "456 Elm St",
                            RecieveNewsLetter = true
                        },
                        new PersonAddRequest
                        {
                            PersonName = "Bob Jothnson",
                            Email = "bob.johnson@example.com",
                            DateOfBirth = new DateTime(1985, 8, 20),
                            Gender = GenderOptions.Male,
                            CountryId = country_from_AddCountry2.CountryId,
                            Address = "789 Maple St",
                            RecieveNewsLetter = false
                        },
                        new PersonAddRequest
                        {
                            PersonName = "Charlie Brown",
                            Email = "charlie.brown@example.com",
                            DateOfBirth = new DateTime(2000, 12, 25),
                            Gender = GenderOptions.Other,
                            CountryId = country_from_AddCountry1.CountryId,
                            Address = "101 Pine St",
                            RecieveNewsLetter = true
                        },
                    };

            List<PersonResponse> personResponses_from_AddPerson = new List<PersonResponse>();

            foreach (PersonAddRequest personAddRequest in personAddRequests)
            {
                PersonResponse personResponse_from_AddPerson = _personService.AddPerson(personAddRequest);
                personResponses_from_AddPerson.Add(personResponse_from_AddPerson);
            }
            return personResponses_from_AddPerson;
        }

        #endregion
    }
}
