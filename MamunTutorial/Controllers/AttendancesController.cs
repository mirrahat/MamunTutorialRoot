using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MamunTutorial.Data;
using MamunTutorial.Models;
using MamunTutorial.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MamunTutorial.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AttendancesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendence()
        {
            return await _context.Attendence.ToListAsync();
        }

        // GET: api/Attendances/id
        [HttpGet("id")]
        public async Task<ActionResult<Attendance>> GetAttendance([FromQuery] Guid id)
        {
            var attendance = await _context.Attendence.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        [HttpGet("allAttendances")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllAttendances()
        {
            return await _context.Attendence.ToListAsync();
        }


        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(Guid id, Attendance attendance)
        {
            if (id != attendance.AttendenceId)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/Attendances
        [AllowAnonymous]
[HttpPost]
        [HttpPost]
        public async Task<IActionResult> PostAttendance([FromBody] List<AttendanceDTO> attendanceList)
        {
            if (attendanceList == null || !attendanceList.Any())
            {
                return BadRequest("Attendance data is required.");
            }

            // Map AttendanceDTO to Attendance entities and prepare them for insertion
            var attendanceEntities = attendanceList.Select(dto => new Attendance
            {
                AttendenceId = Guid.NewGuid(), // Generate a new GUID for each record
                StudentId = dto.StudentId,    // Ensure this is correctly passed as a GUID
                IsPresent = dto.IsPresent,
               
                Date = DateTime.UtcNow,  // Example: Add a timestamp if needed
               Class=dto.SelectedClass
            }).ToList();

            // Insert into the database
            await _context.Attendence.AddRangeAsync(attendanceEntities);

            // Save changes to persist data
            await _context.SaveChangesAsync();

            return Ok("Attendance records successfully added.");
        }



        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(Guid id)
        {
            var attendance = await _context.Attendence.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendence.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(Guid id)
        {
            return _context.Attendence.Any(e => e.AttendenceId == id);
        }
    }
}
