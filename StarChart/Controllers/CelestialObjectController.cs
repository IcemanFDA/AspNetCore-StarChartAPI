using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int Id)
        {
            var celestialObject = _context.CelestialObjects.SingleOrDefault(c => c.Id == Id);

            if (celestialObject == null)
                return NotFound();

            var satelites = from c in _context.CelestialObjects
                            where c.OrbitedObjectId == celestialObject.Id
                            select c;

            celestialObject.Satellites = satelites.ToList();
            return Ok(celestialObject);
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string Name)
        {
            var celestialObjects = from c in _context.CelestialObjects 
                                  where c.Name == Name
                                  select c;

            if (!celestialObjects.Any())
                return NotFound();

            foreach (CelestialObject celObj in celestialObjects)
            {
                var satelites = from c in _context.CelestialObjects
                                where c.OrbitedObjectId == celObj.Id
                                select c;

                celObj.Satellites = satelites.ToList();
            }

            return Ok(celestialObjects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            foreach (CelestialObject celestialObject in _context.CelestialObjects)
            {
                celestialObject.Satellites = (from c in _context.CelestialObjects where c.OrbitedObjectId == celestialObject.Id select c).ToList();
            }
            return Ok(_context.CelestialObjects);
        }
    }
}
