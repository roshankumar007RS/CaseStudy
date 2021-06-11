using Case_Study.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_Study.Services
{
    public interface IDoctorService
    {
        Task<ActionResult<List<Doctor>>> GetDoctorBySpecialization(string specialization);
        Task<ActionResult<Doctor>> PostDoctorByService(Doctor doctor);
        Task<ActionResult<Doctor>> GetDoctorByEmail(string email);
    }
}
