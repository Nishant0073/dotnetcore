using System.Net.NetworkInformation;
using Microsoft.Extensions.FileProviders;

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
// enabled static files, works with wwwroot folder
app.UseStaticFiles();

//for another static folder 
app.UseStaticFiles(new StaticFileOptions(){
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"mywebroot"))
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
    endpoints.Map("/",async(context) => {
        context.Response.WriteAsync("Hello!");
    });
});


app.Run();
