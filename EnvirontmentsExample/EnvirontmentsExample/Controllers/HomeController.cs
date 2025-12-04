using Microsoft.AspNetCore.Mvc;

namespace EnvirontmentsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
