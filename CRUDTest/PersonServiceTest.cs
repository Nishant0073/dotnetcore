using Entities;
using ServiceContracts.DTOs;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest
{
    public class PersonServiceTest
    {
        PersonService _personService;
        CountryService _countryService;
        public PersonServiceTest()
        {
            _personService = new PersonService();
            _countryService = new CountryService();
            
        }
        #region AddPerson

        //when person is null should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullRequest()
        {
            // Arrange
            PersonAddRequest personRequestAdd = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personRequestAdd));
        }


        //when person name is null should throw ArgumentException
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


        //when person is valid should return PersonResponse
        [Fact]
        public void AddPerson_ValidRequest() {
            // Arrange
            PersonAddRequest personRequestAdd = new PersonAddRequest
            {
                PersonName = "John Doe",
                Email = "john@mail.com",
                DateOfBirth = new DateTime(2002, 1, 1),
                Gender = ServiceContracts.Enum.GenderOptions.Male,
                CountryId = Guid.NewGuid(),
                Address = "123 Main St",
                RecieveNewsLetter = true
            };
            // Act 
            var response_from_add_person = _personService.AddPerson(personRequestAdd);
            var response_from_getAllPerson = _personService.GetAllPersons();

            // Assert
            Assert.True(response_from_add_person.PersonId!=Guid.Empty);
            Assert.Contains(response_from_add_person, response_from_getAllPerson);

        }
        #endregion

        #region GetPersonById
        [Fact]
        public void GetPersonById_NullPersonId()
        {
            //Arrange & Act
            PersonResponse? personResponse_from_GetPersonById = _personService.GetPersonById(null);

            Assert.Null(personResponse_from_GetPersonById);
        }

        [Fact]
        public void GetPersonById_ValidPersonId()
        {
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
                Gender = ServiceContracts.Enum.GenderOptions.Male,
                CountryId = country_from_AddCountry.CountryId,
                Address = "123 Main St",
                RecieveNewsLetter = true
            };

           PersonResponse personResponse_from_AddPerson = _personService.AddPerson(personAddRequest);

            PersonResponse personResponse_from_GetPersonById = _personService.GetPersonById(personResponse_from_AddPerson.PersonId);

            Assert.Equal(personResponse_from_AddPerson.PersonId, personResponse_from_GetPersonById.PersonId);
        }
        #endregion
    }
}
