using ConfigurationExample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// supply an object of WeatherApiOptions (with 'weatherapi' section) as a service
builder.Services.Configure<WeatherApiOptions>(builder
    .Configuration.GetSection("weatherapi"));

//load myownconfig.json
builder.Configuration.AddJsonFile("MyOwnConfig.json", optional: true, reloadOnChange: true);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoint =>
{
    //_ = endpoint.Map("/", async context =>
    _ = endpoint.Map("/config", async context =>
    {
        await context.Response.WriteAsync(app.Configuration["MyKey"] + "\n");
        await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey") + "\n");
        await context.Response.WriteAsync(app.Configuration.GetValue<int>("x", 10) + "\n");
        await context.Response.WriteAsync(app.Configuration.GetValue<int>("UnfoundKey", 10) + "\n");
    });
});

app.MapControllers();

app.Run();
