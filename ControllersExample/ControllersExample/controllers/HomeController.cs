using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController
    {
        // multiple route
        [Route("home")]
        [Route("/")]
        public string Index()
        {
            return "hello from index";
        }

        [Route("about")]
        public string About()
        {
            return "hello from about";
        }

        [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "hello from contact";
        }
    }
}
