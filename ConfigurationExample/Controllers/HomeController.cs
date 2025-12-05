using Microsoft.AspNetCore.Mvc;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        // private field
        private readonly IConfiguration _configuration;

        //constructor
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.MyKey = _configuration["MyKey"];
            ViewBag.MyAPIKey = _configuration.GetValue("MyAPIKey", "Key not found, so it's Default value");

            //ViewBag.ClientID = _configuration["weatherapi:ClientID"];
            //ViewBag.ClientSecret = _configuration.GetValue("weatherapi:ClientSecret", "Key has been founded, so it will be ignored");

            // alternative
            IConfiguration weatherapiSection = _configuration.GetSection("weatherapi");
            ViewBag.ClientID = weatherapiSection["ClientID"];
            ViewBag.ClientSecret = weatherapiSection["ClientSecret"];

            return View();
        }
    }
}
