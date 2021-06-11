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
    public class PatientService: IPatientService
    {
        private readonly PatientContext _context;
        public PatientService(PatientContext patientContext)
        {
            _context = patientContext;
        }
        public async Task<ActionResult<Patient>> PostPatientByService(Patient patient)
        {
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return patient;
        }
        public async Task<ActionResult<Patient>> GetPatientByEmail(string email)
        {
            var patient =await _context.Patient.Where(x => x.Email == email).FirstOrDefaultAsync();

            return patient;
        }

    }
}
