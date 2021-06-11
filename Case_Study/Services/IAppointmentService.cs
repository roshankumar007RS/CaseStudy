using Case_Study.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Case_Study.Services
{
    public interface IAppointmentService
    {
        Task<ActionResult<List<Appointment>>> GetAppointmentByPatientID(int patientID);
        Task<ActionResult<Appointment>> PostAppointmentInService(Appointment appointment);
        Task<ActionResult<List<Appointment>>> GetAppointmentByDoctorID(int doctor_id);
        Task<ActionResult<Appointment>> GetAppointmentByID(int id);

        HttpResponseMessage PutAppointment(int id, Appointment appointment);
        HttpResponseMessage PostAppointmentForReview(int id, string review);

        HttpResponseMessage DeleteAppointment(int id);


    }
}
