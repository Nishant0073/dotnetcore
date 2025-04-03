namespace StockApi.Service
{
    public class StockService
    {
        private IHttpClientFactory _httpClient;
        public StockService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetStockPrice(string stockSymbol)
        {
            using (var client = _httpClient.CreateClient("StockApi"))
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"stock/{stockSymbol}/quote");
                var response =  await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Error fetching stock price");
                }
            }
        }
    }
}
