using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Case_Study.Data;
using Case_Study.Models;
using Case_Study.Services;

namespace Case_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (patient.Name == null || patient.Contact == 0 || patient.Email == null || patient.Password == null)
            {
                return BadRequest();
            }
            var patient1 = await _patientService.PostPatientByService(patient);
            return patient1;
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<Patient>> GetPatient(string email)
        {
            var patient = await _patientService.GetPatientByEmail(email);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }




        //    private readonly PatientContext _context;

        //    public PatientsController(PatientContext context)
        //    {
        //        _context = context;
        //    }

        //    // GET: api/Patients
        //    [HttpGet]
        //    public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        //    {
        //        return await _context.Patient.ToListAsync();
        //    }

        //    // GET: api/Patients/5

        //    [HttpGet("{email}")]
        //    public async Task<ActionResult<Patient>> GetPatient(string email)
        //    {
        //        var patient = await _context.Patient.Where(x => x.Email == email).FirstOrDefaultAsync();



        //        if (patient == null)
        //        {
        //            return NotFound();
        //        }



        //        return patient;
        //    }





        //    // PUT: api/Patients/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for
        //    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutPatient(int id, Patient patient)
        //    {
        //        if (id != patient.PatientID)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(patient).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PatientExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Patients
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for
        //    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //    [HttpPost]
        //    public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        //    {
        //        _context.Patient.Add(patient);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetPatient", new { id = patient.PatientID }, patient);
        //    }

        //    // DELETE: api/Patients/5
        //    [HttpDelete("{id}")]
        //    public async Task<ActionResult<Patient>> DeletePatient(int id)
        //    {
        //        var patient = await _context.Patient.FindAsync(id);
        //        if (patient == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Patient.Remove(patient);
        //        await _context.SaveChangesAsync();

        //        return patient;
        //    }

        //    private bool PatientExists(int id)
        //    {
        //        return _context.Patient.Any(e => e.PatientID == id);
        //    }
    }
}
