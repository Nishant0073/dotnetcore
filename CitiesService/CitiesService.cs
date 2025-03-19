namespace CitiesService
{
    public class MyCitiesService
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
