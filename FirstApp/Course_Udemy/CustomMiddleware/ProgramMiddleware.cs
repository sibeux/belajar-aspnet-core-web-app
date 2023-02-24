//using FirstApp.Course_Udemy.CustomMiddleware;
//using Microsoft.Extensions.Primitives;
//using System.Xml.Linq;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddTransient<MyCustomMiddleware>();

//var app = builder.Build();

////Use when
//app.UseWhen(
//    context => context.Request.Query.ContainsKey("username"),
//    app =>
//    {
//        app.Use(async (context, next) =>
//        {
//            await context.Response.WriteAsync($"\nHello from middleware branch!");
//            await next(context);
//        });
//    });

////Middleware 1
//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    await context.Response.WriteAsync("from middleware 1");
//    await next(context);
//});

////Middleware 2
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("\nfrom middleware 2");
//    await next(context);
//});

////Middleware 3
////app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();

////Middleware 4
//app.UseHelloCustomMiddleware();

////MiddlewareFactory 5
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("\nfrom middleware 5");
//});

//app.Run();
