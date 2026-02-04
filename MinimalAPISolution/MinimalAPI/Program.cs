using MinimalAPI.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Product> products = new List<Product>
{
    new Product { Id = 1, ProductName = "Product A" },
    new Product { Id = 2, ProductName = "Product B" },
    new Product { Id = 3, ProductName = "Product C" }
};

// GET /products
app.MapGet("/products", async (HttpContext context) => {
    //var content = string.Join('\n', products.Select(temp => temp.ToString()));
    await context.Response.WriteAsync(JsonSerializer.Serialize(products));
});

// GET /products/{id}
app.MapGet("/products/{id:int}", async (HttpContext context, int id) => {
    Product? product = products.FirstOrDefault(p => p.Id == id);
    if (product != null)
    {
        await context.Response.WriteAsync(JsonSerializer.Serialize(product));
    }
    else
    {
        context.Response.StatusCode = 404; // Not Found
        await context.Response.WriteAsync("Product not found.");
    }
});

// POST /products
app.MapPost("/products", async (HttpContext context, Product product) => {
    products.Add(product);
    await context.Response.WriteAsync("Product added successfully.");
});

app.Run();
