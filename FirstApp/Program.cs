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

    // endpoints file
    endpoints.Map("files/{filename}.{extension}", async (context) =>
    {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
        await context.Response.WriteAsync($"\nin files = {fileName}.{extension}");
    });

    // endpoints employee
    endpoints.Map("employee/profile/{employeename=sibe}", async (context) =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"\nin employee = {employeeName}");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request receive at {context.Request.Path}");
});

app.Run();
