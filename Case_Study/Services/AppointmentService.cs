using Case_Study.Data;
using Case_Study.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Case_Study.Services
{
    public class AppointmentService:IAppointmentService
    {
        private readonly AppointmentContext _context;
        public AppointmentService(AppointmentContext appointmentContext)
        {
            _context = appointmentContext;
        }
        public async Task<ActionResult<List<Appointment>>> GetAppointmentByPatientID(int patientID)
        {

            var appointment_list = await _context.Appointment.Where(x => patientID == x.PatientID).ToListAsync();

            return appointment_list;
        }
        public async Task<ActionResult<Appointment>> PostAppointmentInService(Appointment appointment)
        {
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }
        public async Task<ActionResult<List<Appointment>>> GetAppointmentByDoctorID(int doctor_id)
        {

            var appointment_list = await _context.Appointment.Where(x => doctor_id == x.DoctorID).ToListAsync();

            return appointment_list;
        }



        public async Task<ActionResult<Appointment>> GetAppointmentByID(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            return appointment;
        }

        public HttpResponseMessage PutAppointment(int id, Appointment appointment)
        {

            //var appointment_diff = _context.Appointment.FindAsync(id);
            _context.Entry(appointment).State = EntityState.Modified;

            _context.SaveChanges();

            // Appointment appointment_status = _context.Appointment.FirstOrDefault(i => i.AppointmentID == appointment.AppointmentID);
            if (appointment.Prescription != "")
            {
                appointment.Status = "Done";
                _context.SaveChanges();

            }
            _context.SaveChanges();

            var response = new HttpResponseMessage();
            // response.Headers.Add("DeleteMessage", "Succsessfuly Deleted!!!");
            return response;

        }
        public HttpResponseMessage PostAppointmentForReview(int id, string review)
        {

            Appointment appointment = _context.Appointment.Where(x => x.AppointmentID == id).FirstOrDefault();
            var response1 = new HttpResponseMessage(HttpStatusCode.BadRequest);

            if (appointment == null)
            {

                return response1;

            }


            appointment.Review = review;
            _context.Entry(appointment).State = EntityState.Modified;
            _context.SaveChanges();

            var response = new HttpResponseMessage();
            // response.Headers.Add("DeleteMessage", "Succsessfuly Deleted!!!");
            return response;
        }

        public HttpResponseMessage DeleteAppointment(int id)
        {
            var appointment = _context.Appointment.Find(id);
            if (appointment == null)
            {
                var response1 = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response1;
            }


            //_context.Appointment.Remove(appointment);
            //Appointment appointment_status = _context.Appointment.FirstOrDefault(i => i.AppointmentID == appointment.Result.AppointmentID);
            appointment.Status = "Cancelled";
            _context.SaveChanges();

            var response = new HttpResponseMessage();
            // response.Headers.Add("DeleteMessage", "Succsessfuly Deleted!!!");
            return response;

        }

    }
}
