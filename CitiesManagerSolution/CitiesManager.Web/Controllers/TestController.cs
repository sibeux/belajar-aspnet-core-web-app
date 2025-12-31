using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Web.Controllers
{
    // Pakai 1 turunan class CustomControllerBase biar tidak boiler-plate
    //[Route("api/[controller]")]
    //[ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Method()
        {
            return "Hello World";
        }
    }
}
