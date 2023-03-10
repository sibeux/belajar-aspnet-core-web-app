using FirstApp.Course_Udemy.Routing.CustomConstraints;
using Microsoft.Extensions.FileProviders;

// var builder = WebApplication.CreateBuilder(args);

// this builder can access file in the folder myroot and wwwroot
var builder = WebApplication.CreateBuilder(new WebApplicationOptions(){
    WebRootPath = "Course_Udemy/Routing/myroot"
});

// urutan dalam mengakses file dari root path
// 1. wwwroot
// 2. myroot
// 3. myotherroot
// saat ada 2 file dengan nama yang sama, maka yang akan diakses adalah file yang ada di root path pertama

// custom route constraints
builder.Services.AddRouting(option => {
    option.ConstraintMap.Add("months", typeof(MonthCustomConstraints));
});
var app = builder.Build();

// endpoint routing
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

// enable static files
app.UseStaticFiles(); // works with the web root path (myroot)
app.UseStaticFiles(new StaticFileOptions(){
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Course_Udemy/Routing/myotherroot"))
}); // works with "mytoherroot" path

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

    // minlength = minimum length, minlength(3)
    // maxlength = maximum length, maxlength(4)
    // length = minimum and maximum length, length(3,4)
    // length = specific length, length(7)
    // min = minimum value, min(3)
    // max = maximum value, max(4)
    // range = minimum and maximum value, range(1,10)
    // alpha = only alphabet, alpha
    // regex = regular expression, regex(^[a-zA-Z0-9]*$)

    // endpoints employee
    endpoints.Map("employee/profile/{employeename:length(3,4):alpha=sibe}", async (context) =>
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

    // endpoints regex
    endpoints.Map("meeting/{year:int:min(2000)}/{month:regex(^(apr|jul|oct|jan)$)}", async (context) =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if (month == "apr" || month == "jul" || month == "oct" || month == "jan"){
            await context.Response.WriteAsync($"\nin meeting = {year} - {month}");
        } else {
            // this code will never run, because the regex will check the month
            await context.Response.WriteAsync($"\nin meeting = {year} - {month} is not valid");
        }
    });

    // endpoints custom constraints
    // playing/2023/jan
    endpoints.Map("playing/{year:int:min(2000)}/{month:months}", async (context) =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if (month == "apr" || month == "jul" || month == "oct" || month == "jan"){
            await context.Response.WriteAsync($"\nin playing = {year} - {month}");
        } else {
            // this code will never run, because the regex will check the month
            await context.Response.WriteAsync($"\nin playing = {year} - {month} is not valid");
        }
    });

    // endpoints selection orders
    // playing/2023/jan
    endpoints.Map("playing/2023/{month:months}", async (context) =>
    {
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if (month == "apr" || month == "jul" || month == "oct" || month == "jan"){
            await context.Response.WriteAsync($"\nin selection orders = 2023 - {month}");
        } else {
            // this code will never run, because the regex will check the month
            await context.Response.WriteAsync($"\nin selection orders = 2023 - {month} is not valid");
        }
    });

    // static files
    endpoints.MapGet("/", async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Not route matched at {context.Request.Path}");
});

app.Run();
