using Case_Study.Controllers;
using Case_Study.Models;
using Case_Study.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Case_Study.test
{
    public class AppointmentServiceTest
    {
        public Mock<IAppointmentService> mock = new Mock<IAppointmentService>();
        [Fact]
        public async void GetAppointment_ByPatientID_return_list()
        {
            mock.Setup(p => p.GetAppointmentByPatientID(2)).ReturnsAsync(GetTestSessions());

            AppointmentsController app = new AppointmentsController(mock.Object);

            var actual_result = await app.GetAppointmentByPatientID(2);
            Assert.Equal(2, actual_result.Value.Count);


        }
        [Fact]
        public async void GetAppointmentByPatientID_UnknownID_ReturnsNotFoundResultAsync()
        {
            mock.Setup(p => p.GetAppointmentByPatientID(100));
            AppointmentsController app = new AppointmentsController(mock.Object);
            var actual_result = await app.GetAppointmentByPatientID(100);



            Assert.IsType<NotFoundResult>(actual_result.Result);
        }

        [Fact]
        public async void PostAppointment_return_on_succesfull()
        {
            var appointmentDto = new Appointment()
            {
                AppointmentID = 1,
                PatientID = 2,
                DoctorID = 2,
                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            };
            mock.Setup(x => x.PostAppointmentInService(appointmentDto)).ReturnsAsync(appointmentDto);
            var _controller = new AppointmentsController(mock.Object);
            var createdResponse = _controller.PostAppointment(appointmentDto);
            Assert.Equal(1, createdResponse.Result.Value.AppointmentID);
        }



        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var appointmentDto = new Appointment()
            {
                AppointmentID = 1,

                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            };
            mock.Setup(x => x.PostAppointmentInService(appointmentDto));
            var _controller = new AppointmentsController(mock.Object);
            var createdResponse = _controller.PostAppointment(appointmentDto);
            // Assert
            Assert.False(appointmentDto.Equals(createdResponse));
        }
        [Fact]
        public async void GetAppointment_ByDoctorID_ReturnsSameCount()
        {
            mock.Setup(p => p.GetAppointmentByDoctorID(2)).ReturnsAsync(GetTestSessions());

            AppointmentsController app = new AppointmentsController(mock.Object);

            var actual_result = await app.GetAppointmentByDoctorID(2);

            Assert.Equal(2, actual_result.Value.Count);

        }

        [Fact]
        public async void GetAppointmentByDoctorID_UnknownID_ReturnsNotFoundResultAsync()
        {
            mock.Setup(p => p.GetAppointmentByDoctorID(100));
            AppointmentsController app = new AppointmentsController(mock.Object);
            var actual_result = await app.GetAppointmentByDoctorID(100);

            Assert.IsType<NotFoundResult>(actual_result.Result);
        }

        [Fact]
        public async void GetAppointment_ByDoctorID_ReturnsUnEqualCount()
        {
            mock.Setup(p => p.GetAppointmentByDoctorID(2)).ReturnsAsync(GetTestSessions());

            AppointmentsController app = new AppointmentsController(mock.Object);

            var actual_result = await app.GetAppointmentByDoctorID(2);
            Assert.NotEqual(10, actual_result.Value.Count);
        }



        [Fact]
        public async void GetAllAppointmentsByAppointmentID()
        {
            int appointmentId = 1;

            mock.Setup(p => p.GetAppointmentByID(appointmentId)).ReturnsAsync(new Appointment());

            AppointmentsController app = new AppointmentsController(mock.Object);
            var appointmentDetail = await app.GetAppointment(1);
            var newAppointment = new Appointment()
            {
                AppointmentID = 4,
                PatientID = 2,
                DoctorID = 2,
                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            };

            Assert.Equal(newAppointment.Date, appointmentDetail.Value.Date);
        }

        [Fact]
        public async void GetAllAppointmentsByAppointmentIDNotFound()
        {
            int appointmentId = 100;

            mock.Setup(p => p.GetAppointmentByID(appointmentId));

            AppointmentsController app = new AppointmentsController(mock.Object);
            var appointmentDetail = await app.GetAppointment(100);

            Assert.IsType<NotFoundResult>(appointmentDetail.Result);
        }

        [Fact]
        public async void UpdateAppointment_AddPrescriptionSuccesfully()
        {
            var newAppointment = new Appointment()
            {
                AppointmentID = 4,
                PatientID = 2,
                DoctorID = 2,
                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            };
            mock.Setup(p => p.PutAppointment(4, newAppointment));

            AppointmentsController app = new AppointmentsController(mock.Object);
            var actual_result = app.PutAppointment(4, newAppointment);
            Assert.Equal("OK", actual_result.ReasonPhrase);

        }

        [Fact]
        public async void UpdateAppointment_CheckForBadResponse()
        {
            var newAppointment = new Appointment()
            {
                AppointmentID = 4,
                PatientID = 2,
                DoctorID = 2,
                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            };
            mock.Setup(p => p.PutAppointment(343, newAppointment));

            AppointmentsController app = new AppointmentsController(mock.Object);
            var actual_result = app.PutAppointment(343, newAppointment);
            Assert.Equal("Bad Request", actual_result.ReasonPhrase);
        }
        //[Fact]
        //public async void DeleteAppointment_ChangeAppointmentStatusUnSuccesfully()
        //{
        //    var postId = 5;
        //    mock.Setup(p => p.DeleteAppointment(postId));
        //    AppointmentsController app = new AppointmentsController(mock.Object);

        //    //Act
        //    var actual_result = app.DeleteAppointment(postId);
        //    //Assert
        //    Assert.Equal("Bad Request", actual_result.ReasonPhrase);

        //}
        public List<Appointment> GetTestSessions()
        {
            var Appointmentlist = new List<Appointment>();
            Appointmentlist.Add(new Appointment()
            {
                AppointmentID = 1,
                PatientID = 2,
                DoctorID = 2,
                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            });
            Appointmentlist.Add(new Appointment()
            {
                AppointmentID = 1,
                PatientID = 2,
                DoctorID = 5,
                Date = new DateTime(),
                HealthIssue = "string",
                Review = "string",
                Prescription = "Pending"
            });
            return Appointmentlist;
        }
    }
}
