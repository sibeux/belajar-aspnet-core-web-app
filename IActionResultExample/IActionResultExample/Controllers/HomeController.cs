using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        // model binding: route parameter lebih utama daripada query paramter
        [Route("bookstore/{bookid?}/{isloggedin?}")]
        // bookstore/1/false?bookid=10&isloggedin=true
        //public IActionResult Index()
        // Model binding
        public IActionResult Index([FromRoute]int? bookid, [FromQuery]bool? isloggedin)
            // Ambil masing-masing value dari route atau query { bookid = 1, isloggedin = true }
        {
            // Book id should be applied
            //if (!Request.Query.ContainsKey("bookid"))
            // model binding
            if (bookid.HasValue == false)
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supllied");

                // simplified
                //return new BadRequestResult();

                return BadRequest("Book id is not supllied or maybe empty");
            }

            // book id should be 1 to 1000
            //int bookId = Convert.ToUInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookid <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be less than or equeal to zero");

                return BadRequest("Book id can't be less than or equeal to zero");
            }

            if (bookid > 1000)
            {
                //Response.StatusCode = 404;
                //return Content("Book id can't be greather than 1000");

                return NotFound("Book id can't be greather than 1000");
            }

            // isloggedin should be true
            if (isloggedin == false)
            {
                //Response.StatusCode = 401;
                //return Content("User must be authenticated");

                return Unauthorized("User must be authenticated");
            }

            //return File("/sample.pdf", "application/pdf");

            // 302 - found
            // "Books", "Store", maksudnya arahkan ke method Books di controller Store.
            //return new RedirectToActionResult("Books", "Store", new
            //{
            //    id = bookId
            //}); 
            // simplified
            //return RedirectToAction("Books", "Store", new
            //{
            //    id = bookId
            //}); 

            // 301 - permanent redirect
            //return new RedirectToActionResult("Books", "Store", new { }, true); 
            //return new RedirectToActionResult("Books", "Store", new { }, permanent: true); 
            //return RedirectToActionPermanent("Books", "Store", new
            //{
            //    id = bookId
            //});

            // 302 - found
            //return new LocalRedirectResult($"store/books/{bookId}");
            //return LocalRedirect(($"store/books/{bookId}"));

            // 301 - moved permanently
            //return new LocalRedirectResult($"store/books/{bookId}", true);
            //return LocalRedirectPermanent(($"store/books/{bookId}"));

            // Redirect bisa diarahkan ke web luar, eg: medsos/web apapun
            // 302 - found
            //return Redirect($"store/books/{bookId}");
            // 301 - moved permanently
            //return RedirectPermanent($"store/books/{bookid}");

            return Content($"BookID: {bookid}", "text/plain");
        }
    }
}
