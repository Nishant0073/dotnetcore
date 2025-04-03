namespace StockApi.Models
{
    public class Stock
    {
        public string symbol { get; set; }
        public double CurrentPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double OpenPrice { get; set; }
        public double PreviousClosePrice { get; set; }
    }
}
