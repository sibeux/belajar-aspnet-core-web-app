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

    // endpoints products
    endpoints.Map("products/details/{id:int?}", async (context) =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int? productId = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"\nin products = {productId}");
        } else {
            await context.Response.WriteAsync($"\nin products = no product name");
        }
    });

    // endpoints date time
    endpoints.Map("date/{year:datetime}", async (context) =>
    {
        DateTime year = Convert.ToDateTime(context.Request.RouteValues["year"]);
        await context.Response.WriteAsync($"\nin date = {year.ToShortDateString()}");
    });

    // endpoints GUID
    endpoints.Map("guid/{id:guid}", async (context) =>
    {
        Guid id = Guid.Parse(Convert.ToString(context.Request.RouteValues["id"]));
        await context.Response.WriteAsync($"\nin guid = {id}");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request receive at {context.Request.Path}");
});

app.Run();
