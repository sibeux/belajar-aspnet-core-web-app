using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace DIPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService _usersService;

        public HomeController(IUsersService usersService) { 
            _usersService = usersService;
        }

        [Route("user/{name}")]
        public IActionResult Index(string name)
        {
            string text = _usersService.getMyName(name);
            return Content(text, "text/plain");
        }
    }
}
