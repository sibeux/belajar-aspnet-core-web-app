using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(
    typeof(ICitiesService),
    typeof(CitiesService),
    // berbeda di tiap request
    //ServiceLifetime.Transient 
    // sama di tiap request
    ServiceLifetime.Scoped
// hidup selamanya, selama aplikasi tidak direstart
//ServiceLifetime.Singleton
));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
