namespace middleware.MyMiddlewares;

public class CustomeMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("Custome middleware start\n");
        await next(context);
        await context.Response.WriteAsync("Custome middleware ends\n");
    }
    
}

public static class CustomeMiddlewareExtenstion{
    public static IApplicationBuilder UseMyCustomerMiddleWare(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomeMiddleware>();
        return app;
    }
}
