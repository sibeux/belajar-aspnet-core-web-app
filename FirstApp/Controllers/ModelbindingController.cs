using Microsoft.AspNetCore.Mvc;
using FirstApp.Models;

namespace FirstApp.Controller
{
    [Controller] // optional if class name ends with "Controller"
    public class ModelBindindController : Microsoft.AspNetCore.Mvc.Controller
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
                , "application/pdf"
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

        // urutan model binding
        // 1. Form Fiels
        // 2. Request body
        // 3. Route Data
        // 4. Query String

        // apabila pada route terdapat parameter, maka value dari parameter ini akan diambil daripada query string
        [Route("/bookmodel/{bookid?}/{isloggedin?}/{namebook?}/{page?}")]
        public IActionResult BookIndex(int? bookid, bool? isloggedin,
            [FromQuery] String? namebook,[FromRoute] int? page)
        {
            // book id should be supplied
            if (bookid.HasValue == false)
            {
                //Response.StatusCode = 400;
                //return Content("book id is not supplied");
                return BadRequest("book id is not supplied");
            }

            // book id can't be less the or equal to zero
            if (bookid <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be null or empty");
                return BadRequest("Book id can't be less then or equal to zero");
            }

            // book id should be between 1 to 1000
            if (bookid <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("book id can't be less than or equal to zero");
                return NotFound("book id can't be less than or equal to zero");
            }
            if (bookid > 1000)
            {
                //Response.StatusCode = 400;
                //return Content("book id can't be greater than 1000");
                return NotFound("book id can't be greater than 1000");
            }

            // user must be logged in
            if (isloggedin == false)
            {
                // Response.StatusCode = 401
                //return Content("the user must be logged in");
                return Unauthorized("the user must be logged in");
                //return StatusCode(401);
            }
            return Content($"bookid : {bookid}\n" +
                $"isloggedin : {isloggedin}\n" +
                $"namebook : {namebook}\n" +
                $"page : {page}", "text/plain");
          
        }
    }
}