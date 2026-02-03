using Asp.Versioning;
using CitiesManager.Web.DatabaseContext;
using CitiesManager.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CitiesManager.Web.Controllers.v2
{
    // Pakai 1 turunan class CustomControllerBase biar tidak boiler-plate
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiVersion("2.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of cities (only city name) from 'cities' table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // Untuk ignore global filters dan set secara spesifik di method endpoint.
        //[Produces("application/xml")]
        // Jika tidak diberi atribute [Http{method}], maka dia otomatis lihat nama function-nya.
        // Kalau ada GetFunc, PutFunc, PostFunc, DeleteFunc, maka dia akan otomatis jadi endpoint GET, PUT, POST, DELETE.
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            var cities = await _context.Cities
                .OrderBy(c => c.CityName)
                .Select(c => c.CityName)
                .ToListAsync();
            return cities;
        }
    }
}
