using CitiesServiceContract;

namespace CitiesService
{
    public class MyCitiesService: ICitiesService
    {
        private List<string> cities;
        public MyCitiesService()
        {
            cities = new List<string>()
            {
                "New York",
                "Los Angeles",
                "Chicago",
                "Houston",
            };
        }

        public List<string> GetCities()
        {
            return cities;
        }

    }
}
