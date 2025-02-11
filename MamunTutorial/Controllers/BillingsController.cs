using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MamunTutorial.Data;
using MamunTutorial.Models;

namespace MamunTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BillingsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Billings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Billing>>> GetBillings()
        {
            return await _context.Billings.ToListAsync();
        }

        // GET: api/Billings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Billing>> GetBilling(Guid id)
        {
            var billing = await _context.Billings.FindAsync(id);

            if (billing == null)
            {
                return NotFound();
            }

            return billing;
        }

        // PUT: api/Billings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilling(Guid id, Billing billing)
        {
            if (id != billing.BillingId)
            {
                return BadRequest();
            }

            _context.Entry(billing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingExists(id))
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

        // POST: api/Billings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Billing>> PostBilling(Billing billing)
        {
            _context.Billings.Add(billing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBilling", new { id = billing.BillingId }, billing);
        }

        // DELETE: api/Billings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilling(Guid id)
        {
            var billing = await _context.Billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }

            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingExists(Guid id)
        {
            return _context.Billings.Any(e => e.BillingId == id);
        }
    }
}
