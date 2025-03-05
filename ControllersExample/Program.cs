var builder = WebApplication.CreateBuilder(args);
//register controllers
builder.Services.AddControllers();
var app = builder.Build();

app.UseStaticFiles();
//it will do two things call useRoutingController and useEndpointController
app.MapControllers();

app.Run();
