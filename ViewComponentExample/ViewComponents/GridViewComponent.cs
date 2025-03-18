using Microsoft.AspNetCore.Mvc;
using ViewComponentExample.Models;

namespace ViewComponentExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonGrid grid = new PersonGrid()
            {
                Title = "Employees",
                Persons = new List<Person>()
                {
                    new Person
                    {
                        PersonName = "Shyam",
                        JobTitle = "Manager"
                    },
                    new Person
                    {
                        PersonName = "Ram",
                        JobTitle = "CEO"
                    },
                    new Person
                    {
                        PersonName = "Krishna",
                        JobTitle = "Ass. Manager"
                    },
                }

            };
            ViewData["Grid"] = grid;
            return View("Sample");
        }
    }
}
