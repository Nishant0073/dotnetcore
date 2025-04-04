namespace StockApi.ServiceContracts
{
    public interface IStockService
    {
        public Task<Dictionary<string, Object>> GetStockPriceBySymbol(string symbol);

    }
}
