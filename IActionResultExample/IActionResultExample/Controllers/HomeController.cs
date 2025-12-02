using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        public IActionResult Index()
        {
            // Book id should be applied
            if (!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supllied");

                // simplified
                //return new BadRequestResult();

                return BadRequest("Book id is not supllied");
            }

            // Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be null or empty");

                return BadRequest("Book id can't be null or empty");
            }

            // book id should be 1 to 1000
            int bookId = Convert.ToUInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookId <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be less than or equeal to zero");

                return BadRequest("Book id can't be less than or equeal to zero");
            }

            if (bookId > 1000)
            {
                //Response.StatusCode = 404;
                //return Content("Book id can't be greather than 1000");

                return NotFound("Book id can't be greather than 1000");
            }

            // isloggedin should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                //Response.StatusCode = 401;
                //return Content("User must be authenticated");

                return Unauthorized("User must be authenticated");
            }

            //return File("/sample.pdf", "application/pdf");

            return new RedirectToActionResult("Books", "Store", new
            {

            }); // 302 - found

            // 301 - permanent redirect
            //return new RedirectToActionResult("Books", "Store", new { }, true); 
            //return new RedirectToActionResult("Books", "Store", new { }, permanent: true); 
        }
    }
}
