using Case_Study.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_Study.Services
{
    public interface IPatientService
    {
        Task<ActionResult<Patient>> PostPatientByService(Patient patient);
        Task<ActionResult<Patient>> GetPatientByEmail(string email);

    }
}
