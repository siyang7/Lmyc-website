using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lmyc.Data;
using Lmyc.Models;

namespace Lmyc.Controllers
{
    [Produces("application/json")]
    [Route("api/Volunteers")]
    public class VolunteersAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Volunteers
        [HttpGet]
        public IEnumerable<Volunteer> GetVolunteer()
        {
            return _context.Volunteer;
        }

        // GET: api/Volunteers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolunteer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteer = await _context.Volunteer.SingleOrDefaultAsync(m => m.VoluntterId == id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer);
        }

        // PUT: api/Volunteers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteer([FromRoute] int id, [FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteer.VoluntterId)
            {
                return BadRequest();
            }

            _context.Entry(volunteer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/Volunteers
        [HttpPost]
        public async Task<IActionResult> PostVolunteer([FromBody] Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Volunteer.Add(volunteer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteer", new { id = volunteer.VoluntterId }, volunteer);
        }

        // DELETE: api/Volunteers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteer = await _context.Volunteer.SingleOrDefaultAsync(m => m.VoluntterId == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            _context.Volunteer.Remove(volunteer);
            await _context.SaveChangesAsync();

            return Ok(volunteer);
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteer.Any(e => e.VoluntterId == id);
        }
    }
}