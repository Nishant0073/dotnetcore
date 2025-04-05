using ServiceContracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts
{
    /// <summary>
    /// Represents a request to add a new person.
    /// </summary>
    public class PersonAddRequest
    {
        /// <summary>
        /// Gets or sets the name of the person to be added.
        /// </summary>
        public string? PersonName { get; set; }

        /// <summary>
        /// Gets or sets the email of the person to be added.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the person to be added.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the person to be added.
        /// </summary>
        public GenderOptions? Gender { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the country of the person to be added.
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the address of the person to be added.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person receives the newsletter.
        /// </summary>
        public bool RecieveNewsLetter { get; set; }

        /// <summary>
        /// Converts the current request to a <see cref="Person"/> object.
        /// </summary>
        /// <returns>A <see cref="Person"/> object with the same information as the request.</returns>
        public Person ToPerson()
        {
            Person person = new Person
            {
                PersonName = this.PersonName,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender?.ToString(),
                CountryId = this.CountryId,
                Address = this.Address,
                RecieveNewsLetter = this.RecieveNewsLetter
            };
            return person;
        }
    }
}
