using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts
{
    /// <summary>
    /// Represents a request to add a new country.
    /// </summary>
    public class CountryAddRequest
    {
        /// <summary>
        /// Gets or sets the name of the country to be added.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Converts the current request to a <see cref="Country"/> object.
        /// </summary>
        /// <returns>A <see cref="Country"/> object with the same name as the request.</returns>
        public Country ToCountry()
        {
            return new Country
            {
                CountryName = this.CountryName
            };
        }
    }
}
