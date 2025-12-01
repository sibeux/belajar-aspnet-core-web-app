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

// eg: files/sample.txt
app.Map("files/{filename}.{extension}", async context =>
{
    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"In Files - {fileName}.{extension}");
});

// eg: employee/profile/john
// Default value parameter
app.Map("employee/profile/{EmployeeName=scott}", async context =>
{
    string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
    await context.Response.WriteAsync($"In Employee - {employeeName}");
});

// eg: products/details/1
// Opsional value parameter
app.Map("products/details/{id?}", async context =>
{
    if (context.Request.RouteValues.ContainsKey("id"))
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"Product detail: {id}");
    } else
    {
        await context.Response.WriteAsync("Product details: product detail not set yet");
    }
});

// fallback for any other requests
app.MapFallback(async (context) =>
{   
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
