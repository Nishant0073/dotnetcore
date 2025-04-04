using StockApi;
using StockApi.Service;
using StockApi.ServiceContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IStockService, StockService>();
builder.Services.Configure<TreadingOptions>(builder.Configuration.GetSection(nameof(TreadingOptions))); //add IOptions<TradingOptions> as a service
var app = builder.Build();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();

