using Microsoft.AspNetCore.Mvc;
using ControllersExample.Models;

namespace ControllersExample.Controllers
{
    // ada 3 pendekatan dalam mendeklarasikan class sebagai controller
    // 1. memberikan tag [Controller] dan menamakan file bebas. eg: Home
    // 2. tidak memberikan tag [Controller] dan menamakan file dengan suffix controller. eg: HomeController
    // 3. best practice. beri tag + naming dengan controller
    [Controller]
    public class HomeController : Controller
    {
        // multiple route
        [Route("home")]
        [Route("/")]
        //public string Index()
        public ContentResult Index()
        {
            //return "hello from index";

            //return new ContentResult()
            //{
            //    Content = "Hello from index", ContentType = "text/plain"
            //};

            // ! simplified
            //return Content("Hello from index", "text/plain");

            return Content("<h1>Hello from index</h1>", "text/html");
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "John",
                Age = 25
            };

            //return new JsonResult(person);

            // simplified
            return Json(person);
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/sample.pdf", "application/pdf");

            return File("/sample.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult(@"C:\sibeuxdev\Website\sample.pdf", "application/pdf");

            return PhysicalFile(@"C:\sibeuxdev\Website\sample.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\sibeuxdev\Website\sample.pdf");
            //return new FileContentResult(bytes, "application/pdf");

            return File(bytes, "application/pdf");
        }

        [Route("about")]
        public string About()
        {
            return "hello from about";
        }

        [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "hello from contact";
        }
    }
}
