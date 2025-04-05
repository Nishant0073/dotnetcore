using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Represents a response containing person information.
    /// </summary>
    public class PersonResponse
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
        public string? Gender { get; set; }

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
        /// Gets or sets the age of the person.
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse personResponse = (PersonResponse)obj;

            return personResponse.PersonId == this.PersonId
                && personResponse.PersonName == this.PersonName
                && personResponse.Email == this.Email
                && personResponse.DateOfBirth == this.DateOfBirth
                && personResponse.Gender == this.Gender
                && personResponse.CountryId == this.CountryId
                && personResponse.Address == this.Address
                && personResponse.RecieveNewsLetter == this.RecieveNewsLetter
                && personResponse.Age == this.Age;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Provides extension methods for converting <see cref="Person"/> objects to <see cref="PersonResponse"/> objects.
    /// </summary>
    public static class PersonExtension
    {
        /// <summary>
        /// Converts the specified <see cref="Person"/> object to a <see cref="PersonResponse"/> object.
        /// </summary>
        /// <param name="person">The person to convert.</param>
        /// <returns>A <see cref="PersonResponse"/> object containing the person's information.</returns>
        public static PersonResponse ToPerson(this Person person)
        {
            return new PersonResponse()
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                CountryId = person.CountryId,
                Address = person.Address,
                RecieveNewsLetter = person.RecieveNewsLetter,
                Age = person.DateOfBirth != null ? DateTime.Now.Year - person.DateOfBirth.Value.Year : null
            };
        }
    }
}
