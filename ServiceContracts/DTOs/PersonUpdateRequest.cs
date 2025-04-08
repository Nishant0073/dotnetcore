using Entities;
using ServiceContracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    /// <summary>
    /// Represents a DTO class request to update a person's information.
    /// </summary>
    public class PersonUpdateRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string? PersonName { get; set; }

        /// <summary>
        /// Gets or sets the email of the person.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the person.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the person.
        /// </summary>
        public GenderOptions? Gender { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the country of the person.
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person receives the newsletter.
        /// </summary>
        public bool RecieveNewsLetter { get; set; }

        /// <summary>
        /// A method to convert the DTO to a Person object.
        /// </summary>

        public Person ToPerson()
        {
            return new Person()
            {
                PersonId = PersonId,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                Address = Address,
                RecieveNewsLetter = RecieveNewsLetter
            };
        }
    }
}
