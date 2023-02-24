var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// enable routing
app.UseRouting();

// creating end points
app.UseEndpoints(endpoints =>
{
    // add your end points
    endpoints.MapGet("map1", async (context) =>
    {
        await context.Response.WriteAsync("in map 1");
    });

    endpoints.MapPost("map2", async (context) =>
    {
        await context.Response.WriteAsync("in map 2");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request receive at {context.Request.Path}");
});

app.Run();
