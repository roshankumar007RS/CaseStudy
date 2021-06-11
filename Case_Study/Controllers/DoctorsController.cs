using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Case_Study.Data;
using Case_Study.Models;
using Microsoft.Data.SqlClient;
using Case_Study.Services;

namespace Case_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorservice;

        public DoctorsController(IDoctorService doctorservice)
        {
            _doctorservice = doctorservice;
        }

        [HttpGet("doctor/{specialization}")]
        public async Task<ActionResult<List<Doctor>>> GetDoctor(string specialization)
        {
            var doctor = await _doctorservice.GetDoctorBySpecialization(specialization);
            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
            if (doctor.Name == null || doctor.Contact == 0 || doctor.Email == null || doctor.Password == null || doctor.Specialization == null)
            {
                return BadRequest();
            }
            var doctor1 = await _doctorservice.PostDoctorByService(doctor);
            return doctor1;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<Doctor>> GetDoctorByEmail(string email)
        {
            var doctor = await _doctorservice.GetDoctorByEmail(email);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }


        //private readonly DoctorContext _context;

        //public DoctorsController(DoctorContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Doctors
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctor()
        //{
        //    return await _context.Doctor.ToListAsync();
        //}

        //[HttpGet("doctor/{email}")]
        //public async Task<ActionResult<Doctor>> GetDoctoremail(string email)
        //{
        //    var doctor = await _context.Doctor.Where(x => x.Email == email).FirstOrDefaultAsync();



        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }



        //    return doctor;
        //}




        //// GET: api/Doctors/5
        //[HttpGet("{specialization}")]
        //public async Task<IEnumerable<Doctor>> GetDoctor(string specialization)
        //{
        //    var doctor = await _context.Doctor.Where(x=>x.Specialization==specialization).ToListAsync();


        //    return doctor;
        //}

        //// PUT: api/Doctors/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        //{
        //    if (id != doctor.DoctorID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(doctor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DoctorExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Doctors
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        //{
        //    _context.Doctor.Add(doctor);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDoctor", new { id = doctor.DoctorID }, doctor);
        //}

        //// DELETE: api/Doctors/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        //{
        //    var doctor = await _context.Doctor.FindAsync(id);
        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Doctor.Remove(doctor);
        //    await _context.SaveChangesAsync();

        //    return doctor;
        //}

        //private bool DoctorExists(int id)
        //{
        //    return _context.Doctor.Any(e => e.DoctorID == id);
        //}
    }
}
