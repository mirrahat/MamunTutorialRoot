using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MamunTutorial.Data;
using MamunTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace MamunTutorial.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MarksController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Marks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marks>>> GetMarks()
        {
            return await _context.Marks.ToListAsync();
        }


        [HttpGet("{marksId}/{studentId}")]
        public async Task<ActionResult<Marks>> GetMarks(Guid marksId, Guid studentId)
        {
            var marks = await _context.Marks
                                      .FirstOrDefaultAsync(m => m.MarksId == marksId && m.StudentId == studentId);

            if (marks == null)
            {
                return NotFound("No marks found for the given parameters.");
            }

            return marks;
        }



        // PUT: api/Marks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarks(Guid id, Marks marks)
        {
            if (id != marks.MarksId)
            {
                return BadRequest();
            }

            _context.Entry(marks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarksExists(id))
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

        // POST: api/Marks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Marks>> PostMarks(Marks marks)
        {
            _context.Marks.Add(marks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarks", new { id = marks.MarksId }, marks);
        }

        // DELETE: api/Marks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarks(Guid id)
        {
            var marks = await _context.Marks.FindAsync(id);
            if (marks == null)
            {
                return NotFound();
            }

            _context.Marks.Remove(marks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarksExists(Guid id)
        {
            return _context.Marks.Any(e => e.MarksId == id);
        }
    }
}
