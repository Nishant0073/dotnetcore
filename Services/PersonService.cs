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
    public class PersonService : IPersonService
    {
        private List<Person> _persons;
        private ICountryService _countryService;

        public PersonService()
        {
           _persons = new List<Person>();
           _countryService = new CountryService(); 
        }

        public PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetCountryById(personResponse.CountryId)?.CountryName;
            return personResponse;

        }
        public PersonResponse AddPerson(PersonAddRequest request)
        {
            if(request==null)
                throw new ArgumentNullException(nameof(request));
          
            ValidationHelpers.ModelValidation(request);


            Person person = request.ToPerson();
            person.PersonId = Guid.NewGuid();
            _persons.Add(person);

            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(p => p.ToPersonResponse()).ToList();
        }

    }
}

