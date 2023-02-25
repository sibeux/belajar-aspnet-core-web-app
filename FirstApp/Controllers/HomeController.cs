using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controller
{
    [Controller] // optional if class name ends with "Controller"
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("home")]
        [Route("/")]
        public string Home()
        {
            return "Hello from home";
        }

        [Route("index")]
        public ContentResult Index()
        {
            //return new ContentResult()
            //{
            //    Content = "Hello from index",
            //    ContentType = "text/plain"
            //};

            //return Content("Hello from index", "text/plain");

            return Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");
        }

        [Route("about")]
        public string About()
        {
            return "Hello from about";
        }

        [Route("user/{uid:regex(^\\d{{3}}$)}")]
        public string UID()
        {
            return "Hello from UID";
        }
    }
}