using StockApi.ServiceContracts;
using System.Text.Json;

namespace StockApi.Service
{
    /// <summary>
    /// Service to interact with stock API.
    /// </summary>
    public class StockService : IStockService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client factory.</param>
        public StockService(IHttpClientFactory httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the stock price for the specified stock symbol.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the stock price as a string.</returns>
        /// <exception cref="Exception">Thrown when there is an error fetching the stock price.</exception>
        public async Task<Dictionary<string, Object>> GetStockPriceBySymbol(string symbol)
        {
            using (var client = _httpClient.CreateClient("StockApi"))
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration.GetValue("finnhubKey", "")}");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Stream stream = await response?.Content.ReadAsStreamAsync();
                    StreamReader reader = new StreamReader(stream);
                    string result = reader.ReadToEnd();
                    Dictionary<string, Object> keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, Object>>(result);
                    return keyValuePairs;
                }
                else
                {
                    throw new Exception("Error fetching stock price");
                }
            }
        }

    }
}
