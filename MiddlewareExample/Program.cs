var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("hello");
    await next(context);
});

// middleware 2
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("hello again");
    await next(context);
});

// middleware 3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("hello again");
});

app.Run();
