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
        PersonService personService;
        public PersonServiceTest()
        {
            personService = new PersonService();
            
        }
        #region AddPerson

        //when person is null should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullRequest()
        {
            // Arrange
            PersonAddRequest personRequestAdd = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => personService.AddPerson(personRequestAdd));
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
            Assert.Throws<ArgumentException>(() => personService.AddPerson(personRequestAdd));
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
            var response_from_add_person = personService.AddPerson(personRequestAdd);
            var response_from_getAllPerson = personService.GetAllPersons();

            // Assert
            Assert.True(response_from_add_person.PersonId!=Guid.Empty);
            Assert.Contains(response_from_add_person, response_from_getAllPerson);

        }
        #endregion

    }
}
