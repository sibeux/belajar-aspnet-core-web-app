using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class GameController : Microsoft.AspNetCore.Mvc.Controller   
    {
        [Route("game/console/{id}")]
        public IActionResult Games()
        {
            int id = Convert.ToInt32(Request.RouteValues["id"]);
            return Content($"<h1>Game id {id}</h1>", "text/html");
        }
    }
}
