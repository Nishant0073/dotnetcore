
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class PersonDbContext:DbContext
    {
        public DbSet<Country> Countries { get; set; }   
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            //reading json file for countries
            var countriesJson = System.IO.File.ReadAllText("countries.json");
            var countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            //reading json file for persons
            var personsJson = System.IO.File.ReadAllText("persons.json");
            var persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

            foreach (var country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }

            foreach(var person in persons)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }
        }
    }
}
