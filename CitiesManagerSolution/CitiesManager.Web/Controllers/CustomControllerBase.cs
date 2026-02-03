using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.Web.Controllers
{
    // Pakai route ini jika menggunakan "config.ApiVersionReader = new UrlSegmentApiVersionReader();"
    [Route("api/v{version:apiVersion}/[controller]")]

    //[Route("api/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
    }
}