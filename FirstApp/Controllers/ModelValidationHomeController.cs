
using FirstApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class ModelValidationHomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/register")]
        public IActionResult Index(Person person)
        {
            return Content($"{person}");
        }
    }
}
