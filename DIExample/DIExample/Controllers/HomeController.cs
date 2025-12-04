using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        
        //private readonly CitiesService _citiesService;
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;

        // constructor
        public HomeController(ICitiesService citiesService1, ICitiesService citiesService2, ICitiesService citiesService3)
        {
            // create object of CitiesService class
            _citiesService1 = citiesService1; //new CitiesService();
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
        }
        

        [Route("/")]
        //public IActionResult Index([FromServices] ICitiesService _citiesService)
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();

            ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;

            ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;

            ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;
             
            return View(cities);
        }
    }
}
