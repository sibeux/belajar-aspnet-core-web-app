//using ControllersExample.Controllers;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddTransient<HomeController>();
// shortcut to add controllers
builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
