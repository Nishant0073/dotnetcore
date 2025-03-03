using middleware.MyMiddlewares;

var builder = WebApplication.CreateBuilder(args);

//registering middleware
builder.Services.AddTransient<CustomeMiddleware>();
var app = builder.Build();


//middleware 1
app.Use( async (HttpContext context,RequestDelegate next) => {
    await context.Response.WriteAsync("Hello\n");
    await next(context);
});

//using middleware
//app.UseMiddleware<CustomeMiddleware>();
//app.UseMyCustomerMiddleWare();
app.UseHelloMiddleware();


//middleware 3
app.Use(async (HttpContext context,RequestDelegate next) =>{
    await context.Response.WriteAsync("Hello again\n");
    await next(context);
});

//end middleware
app.Run();