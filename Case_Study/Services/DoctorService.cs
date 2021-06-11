using Case_Study.Data;
using Case_Study.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_Study.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly DoctorContext _context;
        public DoctorService(DoctorContext doctorcontext)
        {
            _context = doctorcontext;
        }
        public async Task<ActionResult<List<Doctor>>> GetDoctorBySpecialization(string specialization)
        {
            var doctor = await _context.Doctor.Where(x => x.Specialization == specialization).ToListAsync();

            return doctor;
        }
        public async Task<ActionResult<Doctor>> PostDoctorByService(Doctor doctor)
        {
            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
        public async Task<ActionResult<Doctor>> GetDoctorByEmail(string email)
        {
            var doctor = await _context.Doctor.Where(x => x.Email == email).FirstOrDefaultAsync();

            return doctor;
        }
    }
}
