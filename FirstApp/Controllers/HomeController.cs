using Microsoft.AspNetCore.Mvc;
using FirstApp.Models;

namespace FirstApp.Controller
{
    [Controller] // optional if class name ends with "Controller"
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("home")]
        [Route("/")]
        public string Home()
        {
            return "Hello from home";
        }

        [Route("index")]
        public ContentResult Index()
        {
            //return new ContentResult()
            //{
            //    Content = "Hello from index",
            //    ContentType = "text/plain"
            //};

            //return Content("Hello from index", "text/plain");

            return Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");
        }

        [Route("about")]
        public string About()
        {
            return "Hello from about";
        }

        [Route("user/{uid:regex(^\\d{{3}}$)}")]
        public string UID()
        {
            return "Hello from UID";
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                id = Guid.NewGuid(),
                firstName = "sibe",
                lastName = "habi",
                age = 20
            };

            //return "{ \"key\" : \"value\" }";
            //return new JsonResult(person);

            // short method
            return Json(person);
        }

        // virtual file result
        [Route("virtual-file")]
        public VirtualFileResult VirtualFileDownload()
        {
            return File(
                "/img1.png",
                "image/png"
                );
        }

        // physical file result
        [Route("physical-file")]
        public PhysicalFileResult PhysicalFileDownload()
        {
            return PhysicalFile(
                @"G:/.shortcut-targets-by-id/1GLhesaekxPRKr-lkNYX-mtfouqJhqhXN/SIBEUX/KULIAH/__UNEJ__/00 - Mata Kuliah/SMT 6 - MB" +
                "/Magang Bersertifikat/Badan Penyelenggara Jaminan Produk Halal/Surat Undangan Onboarding Peserta MSIB (1).pdf"
                ,"application/pdf"
                );
        }

        // file content result
        [Route("content-file")]
        public FileContentResult ContentFileDownload()
        {
            Byte[] bytes = System.IO.File.ReadAllBytes(@"G:/.shortcut-targets-by-id/1GLhesaekxPRKr-lkNYX-mtfouqJhqhXN/SIBEUX/KULIAH/__UNEJ__/00 - Mata Kuliah/SMT 6 - MB" +
                "/Magang Bersertifikat/Badan Penyelenggara Jaminan Produk Halal/Surat Undangan Onboarding Peserta MSIB (1).pdf");
            return File(
                bytes
                , "application/pdf"
                );
        }

        // IActionResult
        // IActionResult adalah parent dari semua method result di atas
        // jadi dengan pake class ini, semua langsung bisa dipakai
        [Route("/book")]
        public IActionResult BookIndex()
        {
            // book id should be supplied
            if (!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("book id is not supplied");
                return BadRequest("book id is not supplied");
            }

            // book id can't empty
            if (String.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be null or empty");
                return BadRequest("Book id can't be null or empty");
            }

            // book id should be between 1 to 1000
            int bookID = Convert.ToInt16(Request.Query["bookid"]);
            if (bookID <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("book id can't be less than or equal to zero");
                return NotFound("book id can't be less than or equal to zero");
            } if (bookID > 1000)
            {
                //Response.StatusCode = 400;
                //return Content("book id can't be greater than 1000");
                return NotFound("book id can't be greater than 1000");
            }

            // user must be logged in
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                // Response.StatusCode = 401
                //return Content("the user must be logged in");
                return Unauthorized("the user must be logged in");
                //return StatusCode(401);
            }

            // REDIRECT PERMANENT VERY DANGER, BECAUSE IT'S HARD TO CHANGE THE URL

            // 302 - found
            // return new RedirectToActionResult("Books", "Store", new {});
            // shorthand
            return RedirectToAction("Books", "Store", new {});

            // 301 - moved permanently
            //return new RedirectToActionResult("Books", "Store", new { }, true);
            // shorthand
            //return RedirectToActionPermanent("Books", "Store", new { id = bookID });
        }

        // local redirect 
        [Route("bookstore")]
        public IActionResult BookStore()
        {
            int bookID = Convert.ToInt32(Request.Query["bookid"]);

            return LocalRedirect($"store/books/{bookID}");

            //return LocalRedirectPermanent($"store/books/2");
        }

        // redirect result
        [Route("game")]
        public IActionResult Game()
        {
            int id = Convert.ToInt32(Request.Query["id"]);

            return Redirect($"game/console/{id}");

            //return RedirectPermanent($"game/console/{id}");
        }

        // redirect to action
        [Route("play")]
        public IActionResult Play()
        {
            int id = Convert.ToInt32(Request.Query["id"]);

            return RedirectToAction("Games", "Game", new { id = id });
        }
    }
}