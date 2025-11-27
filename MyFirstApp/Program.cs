using System.IO;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    //if (1 == 1)
    //{
    //context.Response.StatusCode = 200;

    //} else
    //{
    //context.Response.StatusCode = 500;

    //}
    //context.Response.Headers["MyKey"] = "My value";
    //await context.Response.WriteAsync("Hello");
    //await context.Response.WriteAsync(" World");
    string path = context.Request.Path;
    string method = context.Request.Method;
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync("<h1>Hello</h1>");
    await context.Response.WriteAsync(" <h2>World</h2>");
    await context.Response.WriteAsync($"<p>{path}</p>");
    await context.Response.WriteAsync($"<p>{method}</p>");

    if (method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {
            string id = context.Request.Query["id"];
            await context.Response.WriteAsync($"<p>{id}</p>");
        }
    }

    if (context.Request.Headers.ContainsKey("user-agent"))
    {
        string userAgent = context.Request.Headers["User-Agent"];
        // Modifikasi dari postman
        string authKey  = context.Request.Headers["Auth-Key"];
        await context.Response.WriteAsync($"<p>{userAgent}</p>");
        await context.Response.WriteAsync($"<p>{authKey}</p>");
    }

    // Post method
    StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();

    Dictionary<string, StringValues> queryDict =
        Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

    if (queryDict.ContainsKey("name"))
    {
        string name = queryDict["name"][0];
        await context.Response.WriteAsync(name);
    }
});

app.Run();
