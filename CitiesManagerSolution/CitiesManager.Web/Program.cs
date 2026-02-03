using CitiesManager.Web.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Options di sini berlaku secara global.
    // Explicit set type response into application/json for all web endpoints API
    options.Filters.Add(new ProducesAttribute("application/json"));
    // Explicit set type request body into application/json for all web endpoints API
    options.Filters.Add(new ConsumesAttribute("application/json"));
})
    .AddXmlSerializerFormatters();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer(); // generates description of all web API endpoints/action methods
builder.Services.AddSwaggerGen(options => {
    // include XML comments (from code documentation) in the Swagger JSON and UI
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
}); // generates OpenAPI specification document

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger(); // creates endpoint swagger.json
app.UseSwaggerUI(); // creates swagger UI for testing all web API endpoints/action methods

app.UseAuthorization();

app.MapControllers();

app.Run();
