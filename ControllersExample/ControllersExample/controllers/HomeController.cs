using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    // ada 3 pendekatan dalam mendeklarasikan class sebagai controller
    // 1. memberikan tag [Controller] dan menamakan file bebas. eg: Home
    // 2. tidak memberikan tag [Controller] dan menamakan file dengan suffix controller. eg: HomeController
    // 3. best practice. beri tag + naming dengan controller
    [Controller]
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
