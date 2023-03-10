
using FirstApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class ModelValidationHomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/register")]
        public IActionResult Index(Person person)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n",
                    ModelState.Values.SelectMany(value => value.Errors).Select
                    (err => err.ErrorMessage));

                return BadRequest(errors);
            }
            return Content($"{person}");
        }
    }
}
