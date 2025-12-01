namespace MiddlewareExample.CustomMiddleware;

public class MyCustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("custom middleware - start\n");
        // Setelah dari sini, akan lanjut ke middleware berikutnya, habis itu baru balik lagi ke sini.
        await next(context); 
        await context.Response.WriteAsync("custom middleware - end\n");
    }
}

public static class CustomMiddlewareExtension
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MyCustomMiddleware>();
    }
}
