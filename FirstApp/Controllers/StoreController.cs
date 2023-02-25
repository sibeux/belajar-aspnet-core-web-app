using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class StoreController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("store/books/")]
        public IActionResult Books()
        {
            return Content($"<h1>Book Store</h1>", "text/html");
        }
    }
}
