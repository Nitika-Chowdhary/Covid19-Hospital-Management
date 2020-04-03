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
    public class HospitalsController : ControllerBase
    {
        private readonly Covid19HospitalManagementContext _context;

        public HospitalsController(Covid19HospitalManagementContext context)
        {
            _context = context;
        }

        // Admit patient to a hospital
        [HttpPut]
        [Route("admit/{patientid}/{hospitalid}")]
        public ActionResult Admit(Guid patientid, Guid hospitalid)
        {
            PatientPoco patient = _context.PatientPoco.First(b => b.Id == patientid);
            HospitalPoco hospital = _context.HospitalPocos.First(u => u.Id == hospitalid);

            patient.Hospital = hospital;
            _context.SaveChanges();
            return Ok();
        }


        // Discharge patient from hospital
        [HttpPut]
        [Route("discharge/{patientid}/{hospitalid}")]
        public ActionResult Discharge(Guid patientid, Guid hospitalid)
        {
            PatientPoco patient = _context.PatientPoco.First(b => b.Id == patientid);
            HospitalPoco hospital = _context.HospitalPocos.First(u => u.Id == hospitalid);

            patient.Hospital = null;
            _context.SaveChanges();
            return Ok();
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalPoco>>> GetHospitalPocos()
        {
            return await _context.HospitalPocos.ToListAsync();
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalPoco>> GetHospitalPoco(Guid id)
        {
            var hospitalPoco = await _context.HospitalPocos.FindAsync(id);

            if (hospitalPoco == null)
            {
                return NotFound();
            }

            return hospitalPoco;
        }

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalPoco(Guid id, HospitalPoco hospitalPoco)
        {
            if (id != hospitalPoco.Id)
            {
                return BadRequest();
            }

            _context.Entry(hospitalPoco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalPocoExists(id))
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

        // POST: api/Hospitals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HospitalPoco>> PostHospitalPoco(HospitalPoco hospitalPoco)
        {
            _context.HospitalPocos.Add(hospitalPoco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospitalPoco", new { id = hospitalPoco.Id }, hospitalPoco);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalPoco>> DeleteHospitalPoco(Guid id)
        {
            var hospitalPoco = await _context.HospitalPocos.FindAsync(id);
            if (hospitalPoco == null)
            {
                return NotFound();
            }

            _context.HospitalPocos.Remove(hospitalPoco);
            await _context.SaveChangesAsync();

            return hospitalPoco;
        }

        private bool HospitalPocoExists(Guid id)
        {
            return _context.HospitalPocos.Any(e => e.Id == id);
        }
    }
}
