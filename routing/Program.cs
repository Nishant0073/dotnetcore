var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions(){
        WebRootPath = "myroot"
    }
);
var app = builder.Build();

app.Use(async (context,next) => {
    Endpoint endpoint = context.GetEndpoint();
    await next(context);
});
// enabled static files
app.UseStaticFiles();
app.UseRouting();

app.Use(async (context,next) =>{
    Endpoint endpoint = context.GetEndpoint();
    await next(context);
});

app.UseEndpoints(async endpoints => {
    endpoints.MapGet("map1",async(context) => {
        context.Response.WriteAsync("In map 1");
    });
    endpoints.Map("/",async(context) => {
        context.Response.WriteAsync("Hello!");
    });
});


app.Run();
