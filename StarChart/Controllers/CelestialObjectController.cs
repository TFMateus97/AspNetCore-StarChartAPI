using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

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

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(e => e.Id == id);
            if (celestialObject == null)
                return NotFound();

            var orbitedObject = _context.CelestialObjects.FirstOrDefault(e => e.Id == celestialObject.OrbitedObjectId);
            if (orbitedObject != null)
                celestialObject.Satellites.Add(orbitedObject);

            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(e => e.Name == name);
            if (celestialObject == null)
                return NotFound();

            var orbitedObject = _context.CelestialObjects.FirstOrDefault(e => e.Id == celestialObject.OrbitedObjectId);
            if (orbitedObject != null)
                celestialObject.Satellites.Add(orbitedObject);

            return Ok(celestialObject);

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();
            return Ok(celestialObjects);
        }
    }
}
