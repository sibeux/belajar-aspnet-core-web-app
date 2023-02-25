using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controller
{
    public class HomeController
    {
        [Route("home")]
        [Route("/")]
        public string home()
        {
            return "Hello from home";
        }

        [Route("index")]
        public string index()
        {
            return "Hello from index";
        }

        [Route("about")]
        public string about()
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