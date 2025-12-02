using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// dokumentasi lebih lengkap untuk webroot dan static files, ada di udemy: 48. WebRoot and UseStaticFiles
app.UseStaticFiles(); // untuk wwwroot
app.UseStaticFiles(
    new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
                Path.Combine(
                    builder.Environment.ContentRootPath, "mywebroot"
                    ))
    }); // untuk "mywebroot"

app.MapGet("/", () => "Hello World!");

app.Run();
