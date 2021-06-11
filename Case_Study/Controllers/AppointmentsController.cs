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
using System.Net.Http;
using System.Net;

namespace Case_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("{patientid}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentByPatientID(int patientid)
        {
            var appointment = await _appointmentService.GetAppointmentByPatientID(patientid);
            if (appointment == null)
            {
                return NotFound();
            }


            return appointment;
        }
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            appointment.Status = "Pending";
            if (appointment.DoctorID == 0 || appointment.PatientID == 0 || appointment.HealthIssue == null || appointment.Date == null)
            {
                return BadRequest();
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var appointment1 = await _appointmentService.PostAppointmentInService(appointment);

            return appointment1;
        }

        [HttpGet("doctor/{doctor_id}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentByDoctorID(int doctor_id)
        {
            var appointmentList = await _appointmentService.GetAppointmentByDoctorID(doctor_id);

            if (appointmentList == null)
            {
                return NotFound();
            }
            return appointmentList;
        }

        [HttpGet("appointment/{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {

            var appointment = await _appointmentService.GetAppointmentByID(id);

            if (appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }
        [HttpPut("{id}")]
        public HttpResponseMessage PutAppointment(int id, Appointment appointment)
        {
            var response1 = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var response = new HttpResponseMessage();
            if (id != appointment.AppointmentID)
            {
                return response1;
            }
            try
            {
                var appointment_update = _appointmentService.PutAppointment(id, appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return response;
        }
        [HttpPut("{id}/{review}")]
        public HttpResponseMessage PostAppointmentForReview(int id, string review)
        {

            var appointment_update = _appointmentService.PostAppointmentForReview(id, review);

            return appointment_update;
        }
        [HttpDelete("{id}")]
        public HttpResponseMessage DeleteAppointment(int id)
        {
            var response = new HttpResponseMessage();

            var appointment = _appointmentService.DeleteAppointment(id);
            //if (appointment == null)
            //{
            //    var response1 = new HttpResponseMessage(HttpStatusCode.BadRequest);
            //    return response1;
            //}
            return response;
        }



        //    private readonly AppointmentContext _context;

        //    public AppointmentsController(AppointmentContext context)
        //    {
        //        _context = context;
        //    }

        //    // GET: api/Appointments
        //    [HttpGet]
        //    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointment()
        //    {
        //        return await _context.Appointment.ToListAsync();
        //    }

        //    // GET: api/Appointments/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointment(int id)
        //    {
        //        var appointment = await _context.Appointment.Where(x=>x.PatientID==id).ToListAsync();

        //        if (appointment == null)
        //        {
        //            return NotFound();
        //        }

        //        return appointment;
        //    }
        //    [HttpGet("appointment/{id}")]
        //    public async Task<ActionResult<Appointment>> GetAppointmentfordoctor(int id)
        //    {
        //        var appointment = await _context.Appointment.FindAsync(id);

        //        if (appointment == null)
        //        {
        //            return NotFound();
        //        }

        //        return appointment;
        //    }
        //    [HttpGet("doctor/{doctor_id}")]

        //    public async Task<ActionResult<List<Appointment>>> GetAppointmentByDoctorID(int doctor_id)
        //    {

        //        var appointment_list = await _context.Appointment.Where(x => doctor_id == x.DoctorID).ToListAsync();

        //        if (appointment_list == null)
        //        {
        //            return NotFound();
        //        }
        //        return appointment_list;

        //    }

        //    // PUT: api/Appointments/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for
        //    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        //    {
        //        if (id != appointment.AppointmentID)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(appointment).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();

        //            Appointment appointment_status = _context.Appointment.FirstOrDefault(i => i.AppointmentID == appointment.AppointmentID);
        //            if (appointment_status.Prescription != "")
        //            {
        //                appointment_status.Status = "Done";
        //                await _context.SaveChangesAsync();

        //            }

        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AppointmentExists(id))
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

        //    // POST: api/Appointments
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for
        //    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //    [HttpPost]
        //    public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        //    {
        //        _context.Appointment.Add(appointment);
        //        await _context.SaveChangesAsync();

        //        Appointment appointment_status = _context.Appointment.FirstOrDefault(i => i.AppointmentID == appointment.AppointmentID);

        //        appointment_status.Status = "Pending";
        //        appointment_status.Prescription = "";

        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentID }, appointment);
        //    }
        //    [HttpPut("{id}/{review}")]
        //    public ActionResult<Appointment> PostAppointment(int id,string review)
        //    {
        //        Appointment appointment = _context.Appointment.Where(x=>x.AppointmentID==id).FirstOrDefault();
        //        appointment.Review = review;
        //        _context.Entry(appointment).State = EntityState.Modified;
        //        _context.SaveChanges();

        //        return appointment;
        //    }

        //    // DELETE: api/Appointments/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteAppointment(int id)
        //    {
        //        var appointment = await _context.Appointment.FindAsync(id);
        //        if (appointment == null)
        //        {
        //            return NotFound();
        //        }

        //        Appointment appointment_status = _context.Appointment.FirstOrDefault(i => i.AppointmentID == appointment.AppointmentID);

        //        appointment_status.Status = "Cancelled";
        //        // _context.Appointment.Remove(appointment);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool AppointmentExists(int id)
        //    {
        //        return _context.Appointment.Any(e => e.AppointmentID == id);
        //    }
    }
}
