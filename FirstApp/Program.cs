var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync("Endpoint: " + endpoint.DisplayName + "");
    }
    await next(context);
});

// enable routing
app.UseRouting();

// creating end points
app.UseEndpoints(endpoints =>
{
    app.Use(async (context, next) =>
    {
        Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            await context.Response.WriteAsync("Endpointt: " + endpoint.DisplayName + "\n");
        }
        await next(context);
    });

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
