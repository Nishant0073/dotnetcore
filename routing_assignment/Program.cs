var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> countries = new Dictionary<int, string>(){
 { 1, "United States" },
 { 2, "Canada" },
 { 3, "United Kingdom" },
 { 4, "India" },
 { 5, "Japan" }
};

app.UseEndpoints(endpoint =>
{
    // endpoint.MapGet("/countries", async (context) =>
    // {
    //     foreach (KeyValuePair<int, string> contry in countries)
    //     {
    //         context.Response.WriteAsync(contry.Key.ToString() + ". " + contry.Value.ToString() + "\n");
    //     }
    // });
    endpoint.MapGet("/countries/{countryId:int}", async (context) =>{
        int countryId = Convert.ToInt32(context.Request.RouteValues["countryId"]);

        if(countryId>100){
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("The countryId should be less than 100");
        }
        
        if(countries.ContainsKey(countryId)==false){
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]");
        }
        else{
         await context.Response.WriteAsync(countries[countryId]);
        }
    });
});
app.Run();