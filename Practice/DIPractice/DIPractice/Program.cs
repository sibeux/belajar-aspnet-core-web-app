using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

app.MapAllEndpoints();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
