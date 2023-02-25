using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// add all controllers class as services
builder.Services.AddControllers(); 

var app = builder.Build();
app.MapControllers();

app.Run();