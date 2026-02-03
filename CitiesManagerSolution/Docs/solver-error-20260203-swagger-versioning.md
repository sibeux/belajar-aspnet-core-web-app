# Error Solver Documentation - Swagger Versioning

**Error ID:** `solver-error-20260203-swagger-versioning`  
**Date:** 2026-02-03  
**Project:** CitiesManager.Web  

## Problem Description
Meskipun `options.SwaggerDoc` untuk `v1` dan `v2` sudah ditambahkan di `Program.cs`, Swagger UI hanya menampilkan satu versi saja dan dropdown versi tidak muncul. JSON untuk `v2` sebenarnya tersedia namun tidak terhubung secara otomatis ke UI. Selain itu, terdapat isu namespace pada .NET 10/ASP.NET Core 10 terkait `Microsoft.OpenApi.Models`.

## Analysis
1. **Mismatch GroupName**: `AddApiExplorer` menghasilkan GroupName (label) yang berbeda secara format dengan nama dokumen yang diatur di `AddSwaggerGen`. (Contoh: explorer menghasilkan `v1.0` sedangkan doc mengharapkan `v1`).
2. **Missing Predicate**: Swagger tidak tahu bagaimana cara memfilter endpoint mana yang harus masuk ke dokumen `v1` vs `v2` tanpa `DocInclusionPredicate`.
3. **Browser Cache**: Swagger UI menyimpan cache definisi UI yang sangat kuat, sehingga perubahan pada `SwaggerEndpoint` sering tidak langsung terlihat tanpa hard reload.
4. **OpenAPI Conflict**: Fitur built-in OpenAPI di .NET 10 (`AddOpenApi`) terkadang berkonflik dengan rute default Swashbuckle.

## Solution
1. **Sync Format**: Mengubah `GroupNameFormat` menjadi `'v'V` agar menghasilkan `v1`, `v2`, dst.
2. **DocInclusionPredicate**: Menambahkan logic filter untuk mencocokkan `apiDesc.GroupName` dengan `docName`. Menambahkan fallback agar endpoint tanpa versi eksplisit tetap muncul di `v1`.
3. **Disable Built-in OpenAPI**: Menonaktifkan `AddOpenApi()` dan `MapOpenApi()` untuk menghindari kebingungan rute JSON.
4. **Relative Path for UI**: Menggunakan path relatif (misal: `"v1/swagger.json"`) pada `UseSwaggerUI` agar lebih fleksibel dan mudah dibaca oleh UI.

## Code Changes Tags
Cari tag berikut di `Program.cs`:
`[error-solve-20260203-swagger-versioning]`

## Verification
1. Jalankan aplikasi.
2. Akses `/swagger/index.html`.
3. Gunakan **Incognito Mode** atau **Hard Reload (Ctrl + F5)** jika dropdown belum muncul.
4. Pilih versi dari dropdown di pojok kanan atas.
