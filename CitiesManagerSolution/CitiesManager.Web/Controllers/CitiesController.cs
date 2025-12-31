using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Web.DatabaseContext;
using CitiesManager.Web.Models;

namespace CitiesManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        // Jika tidak diberi atribute [Http{method}], maka dia otomatis lihat nama function-nya.
        // Kalau ada GetFunc, PutFunc, PostFunc, DeleteFunc, maka dia akan otomatis jadi endpoint GET, PUT, POST, DELETE.
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{cityID}")]
        public async Task<ActionResult<City>> GetCity(Guid cityID)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(temp => temp.CityId == cityID);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cityID}")]
        public async Task<IActionResult> PutCity(Guid cityID, [Bind(nameof(City.CityId), nameof(City.CityName))] City city)
        {
            if (cityID != city.CityId)
            {
                return BadRequest(); // http 400
            } 

            //_context.Entry(city).State = EntityState.Modified;
            var existingCity = await _context.Cities.FindAsync(cityID);

            if (existingCity == null)
            {
                return NotFound(); // http 404
            }

            existingCity.CityName = city.CityName;
            // Tambahkan properti lain yang perlu diupdate di sini

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityId), nameof(city.CityName))] City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityID = city.CityId }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound(); // http 404
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent(); // http 200
        }

        private bool CityExists(Guid id)
        {
            return _context.Cities.Any(e => e.CityId == id);
        }
    }
}
