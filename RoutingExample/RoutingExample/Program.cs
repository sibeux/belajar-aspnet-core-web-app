var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// routing is automatically enabled.
// no need for app.UseRouting() anymore.

// Endpoints are defined directly on the "app" object.

app.MapGet("map1", async (context) =>
{
    await context.Response.WriteAsync("in map 1");
});

app.MapPost("map2", async (context) =>
{
    await context.Response.WriteAsync("in map 2");

});

// fallback for any other requests
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
