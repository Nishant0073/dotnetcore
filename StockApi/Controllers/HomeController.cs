using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApi.Models;
using StockApi.Service;
using StockApi.ServiceContracts;

namespace StockApi.Controllers
{
    /// <summary>
    /// Controller to handle requests for the home page.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IOptions<TreadingOptions> _options;

        public HomeController(IStockService stockService,IOptions<TreadingOptions> options)
        {
            _stockService = stockService;
            _options = options;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object> stocks = await _stockService.GetStockPriceBySymbol(_options.Value.DefultOption);
            Stock stock = new Stock()
            {
                symbol = _options.Value.DefultOption,
                CurrentPrice = Convert.ToDouble(stocks["c"].ToString()),
                HighPrice = Convert.ToDouble(stocks["h"].ToString()),
                LowPrice = Convert.ToDouble(stocks["l"].ToString()),
                OpenPrice = Convert.ToDouble(stocks["o"].ToString()),
                PreviousClosePrice = Convert.ToDouble(stocks["pc"].ToString())
            };
            return View(stock);
        }
    }
}
