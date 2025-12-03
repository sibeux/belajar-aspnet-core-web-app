using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.Models;

namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        //Jika ada bind, maka yang seolah-olah diterima adalah atribute ini aja. meskipun atribute yang tidak di-bind sudah diisi, tetap dianggap null.
        //public IActionResult Index([Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Age), nameof(Person.Password), nameof(Person.ConfirmPassword))] Person person)
        public IActionResult Index(Person person)
        {
            if (!ModelState.IsValid)
            {
                //List<string> errorList = new List<string>();
                //foreach (var value in ModelState.Values)
                //{
                //    foreach (var error in value.Errors)
                //    {
                //        errorList.Add(error.ErrorMessage);
                //    }
                //}

                // simplified
                // atau gunakan var jika gak tau tipe datanya
                IEnumerable<string> errorList =  ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage);

                string errors = string.Join("\n", errorList);
                return BadRequest(errors);
            }

            return Content($"{person}");
        }
    }
}
