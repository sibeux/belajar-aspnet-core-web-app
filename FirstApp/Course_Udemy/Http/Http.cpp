using Microsoft.Extensions.Primitives;

if (1 == 1)
{
   context.Response.StatusCode = 200;
}
else
{
   context.Response.StatusCode = 404;
}

context.Response.Headers["MyKey"] = "sibeux.value";
context.Response.Headers["Server"] = "sibeux.server";
context.Response.Headers["Content-Type"] = "text/html";

string path = context.Request.Path;
string method = context.Request.Method;

await context.Response.WriteAsync("<h1>Hello World!</h1>");
await context.Response.WriteAsync($"<p>Path = {path}</p>");
await context.Response.WriteAsync($"<p>Method = {method}</p>");

if (method == "GET")
{
   if (context.Request.Query.ContainsKey("id"))
   {
       string id = context.Request.Query["id"];
       string name = context.Request.Query["name"];

       await context.Response.WriteAsync($"<p>ID = {id}</p>");
       await context.Response.WriteAsync($"<p>Name = {name}</p>");
   }

   if (context.Request.Headers.ContainsKey("User-Agent"))
   {
       string userAgent = context.Request.Headers["User-Agent"];

       await context.Response.WriteAsync($"<p>User-Agent = {userAgent}</p>");
   }
}

StreamReader reader = new StreamReader(context.Request.Body);
string body = await reader.ReadToEndAsync();

Dictionary<string, StringValues> bodyDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
if (bodyDict.ContainsKey("firstName"))
{
   string firstName = bodyDict["firstName"][0];
   await context.Response.WriteAsync($"<p>firstName = {firstName}</p>");
}