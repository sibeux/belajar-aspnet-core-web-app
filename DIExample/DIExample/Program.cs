using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();
//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    // berbeda di tiap request
//    //ServiceLifetime.Transient 
//    // sama di tiap request
//    ServiceLifetime.Scoped
//// hidup selamanya, selama aplikasi tidak direstart
////ServiceLifetime.Singleton
//));
//builder.Services.AddTransient<ICitiesService, CitiesService>();
//builder.Services.AddScoped<ICitiesService, CitiesService>();
//builder.Services.AddSingleton<ICitiesService, CitiesService>();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Add transient
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency();
    // add scoped
    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope();
    // Add singleton
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance();
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
