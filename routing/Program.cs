var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context,next) => {
    Endpoint endpoint = context.GetEndpoint();
    await next(context);
});


app.UseRouting();

app.Use(async (context,next) =>{
    Endpoint endpoint = context.GetEndpoint();
    await next(context);
});

app.UseEndpoints(async endpoints => {
    endpoints.MapGet("map1",async(context) => {
        context.Response.WriteAsync("In map 1");
    });
});


app.Run();
