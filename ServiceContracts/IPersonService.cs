using ServiceContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonService
    {
        PersonResponse AddPerson(PersonAddRequest request);
        List<PersonResponse> GetAllPersons();
        PersonResponse? GetPersonById(Guid? personId);
        List<PersonResponse> GetFilteredPersons(string? SearchBy, string? SearchString);
    }
}
