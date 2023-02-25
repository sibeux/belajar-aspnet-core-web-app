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
    }
}