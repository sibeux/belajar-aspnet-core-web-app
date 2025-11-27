var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    //if (1 == 1)
    //{
    //context.Response.StatusCode = 200;

    //} else
    //{
    //context.Response.StatusCode = 500;

    //}
    context.Response.Headers["MyKey"] = "My value";
    //await context.Response.WriteAsync("Hello");
    //await context.Response.WriteAsync(" World");
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync("<h1>Hello</h1>");
    await context.Response.WriteAsync(" <h2>World</h2>");
});

app.Run();
