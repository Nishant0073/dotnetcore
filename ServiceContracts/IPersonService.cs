using ServiceContracts.DTOs;
using ServiceContracts.Enum;
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
        List<PersonResponse> GetSortedPerson(List<PersonResponse> persons, string? SortBy,SortOrderEnum SortOrder);
    }
}
