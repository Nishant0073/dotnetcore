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
using System.Reflection;
using ServiceContracts.Enum;

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
        public PersonResponse ConvertPersonToPersonResponse(Person? person)
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

        #region GetFilteredPersons

        /// <summary>
        /// Retrieves a list of persons filtered by a specified property and search string.
        /// </summary>
        /// <param name="SearchBy">The property to filter by (e.g., "PersonName").</param>
        /// <param name="SearchString">The search string to filter the property by.</param>
        /// <returns>A list of <see cref="PersonResponse"/> objects that match the filter criteria.</returns>
        public List<PersonResponse> GetFilteredPersons(string? SearchBy, string? SearchString)
        {
            List<PersonResponse> all_persons = GetAllPersons();

            if (SearchBy == null || SearchString == null)
                return all_persons;

            List<PersonResponse> filteredPersons = all_persons;

            Type personType = typeof(Person);
            foreach (PropertyInfo property in personType.GetProperties())
            {
                if (property.Name.Equals(SearchBy, StringComparison.OrdinalIgnoreCase))
                {
                    filteredPersons = _persons
                        .Where(p => property.GetValue(p)?.ToString()?.Contains(SearchString, StringComparison.OrdinalIgnoreCase) == true)
                        .Select(p => ConvertPersonToPersonResponse(p))
                        .ToList();
                    break;
                }
            }
            return filteredPersons;
        }

        public List<PersonResponse> GetSortedPerson(List<PersonResponse> persons, string? SortBy, SortOrderEnum SortOrder)
        {
            Type personResponseType = typeof(PersonResponse);
            List<PersonResponse> sortedPersons = persons;
            foreach (PropertyInfo property in personResponseType.GetProperties())
            {
                if(property.Name.Equals(SortBy, StringComparison.OrdinalIgnoreCase))
                {
                    if(SortOrder == SortOrderEnum.ASC)
                    {
                        sortedPersons = persons.OrderBy(p => property.GetValue(p)).ToList();
                    }
                    else if (SortOrder == SortOrderEnum.DSC)
                    {
                        sortedPersons = persons.OrderByDescending(p => property.GetValue(p)).ToList();
                    }
                    break;
                }
            }
            return sortedPersons;
        }

        #endregion
    }
}

