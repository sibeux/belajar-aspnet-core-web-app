using Asp.Versioning;
using CitiesManager.Web.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;


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

// [error-solve-20260203-ambiguous-match] Konfigurasi API Versioning
// Tanpa .AddMvc(), atribut [ApiVersion] di controller tidak akan terbaca oleh sistem routing
// sehingga menyebabkan AmbiguousMatchException karena ada beberapa controller dengan nama yang sama.
builder.Services.AddApiVersioning(config => 
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;

    // Reads version number from request url at "apiVersion" constraint
    // Ex: https://localhost:7254/api/v2/cities
    config.ApiVersionReader = new UrlSegmentApiVersionReader();

    // Reads version number from request query string called "api-version"
    // Ex: https://localhost:7254/api/cities?api-version=2.0
    //config.ApiVersionReader = new QueryStringApiVersionReader();

    // Reads version number from request header called "api-version". Eg: api-version: 1.0
    //config.ApiVersionReader = new HeaderApiVersionReader("api-version");
})
.AddMvc() // MENGHUBUNGKAN Versioning dengan Controller logic (PENTING!)
// [error-solve-20260203-swagger-versioning] Sinkronisasi format nama grup dengan SwaggerDoc
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V"; // Menghasilkan "v1", "v2", dst.
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// builder.Services.AddOpenApi(); // Matikan OpenAPI bawaan agar tidak konflik dengan Swashbuckle

// Swagger
builder.Services.AddEndpointsApiExplorer(); // generates description of all web API endpoints/action methods
// [error-solve-20260203-swagger-versioning] Konfigurasi multi-version SwaggerDoc dan filter endpoint
builder.Services.AddSwaggerGen(options => {
    // include XML comments (from code documentation) in the Swagger JSON and UI
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Cities Web API", Version = "1.0" });
    options.SwaggerDoc("v2", new OpenApiInfo() { Title = "Cities Web API", Version = "2.0" });

// Memberitahu Swagger cara mencocokkan endpoint dengan dokumen versi yang sesuai.
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        // Jika GroupName kosong (misal controller tanpa [ApiVersion]), masukkan ke v1
        if (string.IsNullOrEmpty(apiDesc.GroupName))
        {
            return docName == "v1";
        }
        return apiDesc.GroupName == docName;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger(); // creates endpoint swagger.json
// [error-solve-20260203-swagger-versioning] Menampilkan dropdown multi-versi di UI
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "Cities Web API v1.0");
    options.SwaggerEndpoint("v2/swagger.json", "Cities Web API v2.0");
}); // creates swagger UI for testing all web API endpoints/action methods

app.UseAuthorization();

app.MapControllers();

app.Run();
