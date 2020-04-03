using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Covid19HospitalManagement.EntityFrameworkDataAccess;
using Covid19HospitalManagement.Pocos;

namespace Covid19HospitalMangement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly Covid19HospitalManagementContext _context;

        public PatientsController(Covid19HospitalManagementContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientPoco>>> GetPatientPoco()
        {
            return await _context.PatientPoco.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientPoco>> GetPatientPoco(Guid id)
        {
            var patientPoco = await _context.PatientPoco.FindAsync(id);

            if (patientPoco == null)
            {
                return NotFound();
            }

            return patientPoco;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientPoco(Guid id, PatientPoco patientPoco)
        {
            if (id != patientPoco.Id)
            {
                return BadRequest();
            }

            _context.Entry(patientPoco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientPocoExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PatientPoco>> PostPatientPoco(PatientPoco patientPoco)
        {
            _context.PatientPoco.Add(patientPoco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientPoco", new { id = patientPoco.Id }, patientPoco);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientPoco>> DeletePatientPoco(Guid id)
        {
            var patientPoco = await _context.PatientPoco.FindAsync(id);
            if (patientPoco == null)
            {
                return NotFound();
            }

            _context.PatientPoco.Remove(patientPoco);
            await _context.SaveChangesAsync();

            return patientPoco;
        }

        private bool PatientPocoExists(Guid id)
        {
            return _context.PatientPoco.Any(e => e.Id == id);
        }
    }
}
