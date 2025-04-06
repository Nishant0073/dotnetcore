using ServiceContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using Entities;
using System.ComponentModel.DataAnnotations;
using Services.Helpers;

namespace Services
{
    /// <summary>
    /// Provides services related to person operations.
    /// </summary>
    public class PersonService : IPersonService
    {
        private List<Person> _persons;
        private ICountryService _countryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        public PersonService()
        {
            _persons = new List<Person>();
            _countryService = new CountryService();
        }

        /// <summary>
        /// Converts a <see cref="Person"/> object to a <see cref="PersonResponse"/> object.
        /// </summary>
        /// <param name="person">The person to convert.</param>
        /// <returns>A <see cref="PersonResponse"/> object containing the person's information.</returns>
        public PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryById(personResponse.CountryId)?.CountryName;
            return personResponse;
        }

        #region AddPerson

        /// <summary>
        /// Adds a new person based on the provided request.
        /// </summary>
        /// <param name="request">The request containing the person information to be added.</param>
        /// <returns>A <see cref="PersonResponse"/> object containing the added person's information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="request"/> is null.</exception>
        public PersonResponse AddPerson(PersonAddRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelpers.ModelValidation(request);

            Person person = request.ToPerson();
            person.PersonId = Guid.NewGuid();
            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        #endregion

        #region GetAllPersons

        /// <summary>
        /// Retrieves all persons.
        /// </summary>
        /// <returns>A list of <see cref="PersonResponse"/> objects containing information about all persons.</returns>
        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

        #endregion

        #region GetPersonById

        /// <summary>
        /// Retrieves a person by their unique identifier.
        /// </summary>
        /// <param name="personId">The unique identifier of the person to retrieve.</param>
        /// <returns>A <see cref="PersonResponse"/> object containing the person's information, or null if the person is not found.</returns>
        public PersonResponse? GetPersonById(Guid? personId)
        {
            if (personId == null)
                return null;

            Person? person = _persons.FirstOrDefault(p => p.PersonId == personId);
            if (person == null)
                return null;

            return ConvertPersonToPersonResponse(person);
        }

        #endregion
    }
}

