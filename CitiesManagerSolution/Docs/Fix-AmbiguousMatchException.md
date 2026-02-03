# Fix: AmbiguousMatchException pada API Versioning
**ID: error-solve-20260203-ambiguous-match**

## Masalah
Terjadi error `Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints` pada saat menjalankan aplikasi dengan multiple versi controller (v1 dan v2).

Error ini terjadi meskipun masing-masing controller sudah diberi atribut `[ApiVersion("1.0")]` atau `[ApiVersion("2.0")]`.

## Penyebab
Secara default, ASP.NET Core tidak secara otomatis mengintegrasikan metadata dari paket `Asp.Versioning` ke dalam sistem pemilih endpoint (Endpoint Selector). Jika hanya memanggil `AddApiVersioning()` tanpa konfigurasi tambahan, sistem routing akan melihat dua controller dengan route yang sama (misal: `api/v{version:apiVersion}/Cities`) dan bingung harus memilih yang mana karena ia belum "diajari" cara memfilter berdasarkan versi.

## Solusi
Menambahkan pemanggilan method `.AddMvc()` dan `.AddApiExplorer()` pada konfigurasi layanan API Versioning di `Program.cs`.

### Perubahan pada `Program.cs`
```csharp
// [error-solve-20260203-ambiguous-match] Konfigurasi API Versioning
// Tanpa .AddMvc(), atribut [ApiVersion] di controller tidak akan terbaca oleh sistem routing
// sehingga menyebabkan AmbiguousMatchException karena ada beberapa controller dengan nama yang sama.
builder.Services.AddApiVersioning(config => 
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc() // <--- SOLUSI UTAMA: Mengaktifkan pemfilteran controller berdasarkan versi
.AddApiExplorer(options => // Membantu Swagger mengenali versi API
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
```

### Mengapa ini penting?
1. **`.AddMvc()`**: Method ini mendaftarkan layanan yang diperlukan agar sistem routing ASP.NET Core bisa memahami atribut `[ApiVersion]`. Tanpa ini, sistem tidak tahu versi mana yang dimaksud oleh request URL.
2. **`.AddApiExplorer()`**: Method ini diperlukan agar OpenAPI/Swagger bisa mendeteksi versi-versi yang ada di aplikasi Anda. Ini juga berguna untuk fitur `SubstituteApiVersionInUrl` yang otomatis mengganti placeholder `{version:apiVersion}` di UI Swagger.
