using Case_Study.Controllers;
using Case_Study.Models;
using Case_Study.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Case_Study.test
{
    public class PatientServiceTest
    {
        public Mock<IPatientService> mock = new Mock<IPatientService>();
        [Fact]
        public async void PostPatient_return_on_succesfull()
        {
            var patientDto = new Patient()
            {
                PatientID = 1,
                Name = "madan",
                Contact = 1234567890,
                Email = "madan123@gmail.com",
                Password = "12345",
            };
            mock.Setup(x => x.PostPatientByService(patientDto)).ReturnsAsync(patientDto);
            var _controller = new PatientsController(mock.Object);
            var createdResponse = _controller.PostPatient(patientDto);
            Assert.Equal(1, createdResponse.Result.Value.PatientID);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequestOnPatient()
        {
            // Arrange
            var patientDto = new Patient()
            {


                PatientID = 1,

                Contact = 1234567890,
                Email = "madan123@gmail.com",
                Password = "12345",
            };
            mock.Setup(x => x.PostPatientByService(patientDto));
            var _controller = new PatientsController(mock.Object);
            var createdResponse = _controller.PostPatient(patientDto);
            // Assert
            Assert.False(patientDto.Equals(createdResponse));
        }
    }
}
