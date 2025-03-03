namespace middleware.MyMiddlewares;
public class HelloMiddleware{
    private RequestDelegate _next;
    public HelloMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }

    public async Task Invoke(HttpContext context){
        if(context.Request.Query.ContainsKey("firstName") && context.Request.Query.ContainsKey("lastName")){
             await context.Response.WriteAsync("Hello "+context.Request.Query["firstName"] + " "+ context.Request.Query["lastName"] + "!" );
        }
        await _next(context);
    }
}

public static class HelloMiddlewareExtension{
    public static IApplicationBuilder UseHelloMiddleware(this IApplicationBuilder app){
        app.UseMiddleware<HelloMiddleware>();
        return app;
    }
}

