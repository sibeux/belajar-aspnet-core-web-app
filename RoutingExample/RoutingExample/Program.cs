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
//app.Map("employee/profile/{EmployeeName=scott}", async context =>
// Min Max length
//app.Map("employee/profile/{EmployeeName:minlength(3):maxlength(7)=scott}", async context =>
// alternative min max length
app.Map("employee/profile/{EmployeeName:length(3,7):alpha=scott}", async context =>

{
    string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
    await context.Response.WriteAsync($"In Employee - {employeeName}");
});

// eg: products/details/1
// Opsional value parameter
//app.Map("products/details/{id?}", async context =>
// Route constraints
app.Map("products/details/{id:int?}", async context =>
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

// Eg: daily-digest-report/{reportdate}
app.Map("daily-digest-report/{reportdate:datetime}", async (context) =>
{
    DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);

    await context.Response.WriteAsync($"in daily-digest-report: {reportDate.ToShortDateString()}");
});

// eg: cities/{cityid}
app.Map("cities/{cityid:guid}", async (context) =>
{
    Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
    await context.Response.WriteAsync($"City information - {cityId}");
});

// eg: sales-report/2030/apr
app.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context =>
{
    int year = Convert.ToInt32(context.Request.RouteValues["year"]);
    string? month = Convert.ToString(context.Request.RouteValues["month"]);

    await context.Response.WriteAsync($"Sales report - {year} - {month}");
});

// fallback for any other requests
app.MapFallback(async (context) =>
{   
    await context.Response.WriteAsync($"No route match at {context.Request.Path}");
});

app.Run();
